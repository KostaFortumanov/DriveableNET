using AutoMapper;
using DAL.Dto.Request;
using DAL.Dto.Response;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<IEnumerable<AppUserResponse>> getall()
        {
            IEnumerable<AppUser> users = await _unitOfWork.iappUserRepository.GetAll();
            return _mapper.Map<IEnumerable<AppUser>, IEnumerable<AppUserResponse>>(users);
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(password);
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", password);

            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(password);
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", password);
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", storedHash.ToString());
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", storedSalt.ToString());

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
        public async Task<TokenResponse?> login(LoginAppUserRequest loginRequest)
        {
            AppUser appUser = await _unitOfWork.iappUserRepository.FindByUsername(loginRequest.userName);
            if (appUser != null)
            {
                if (VerifyPasswordHash(loginRequest.password, appUser.PasswordHash, appUser.PasswordSalt))
                {
                    TokenResponse tokenResponse = new TokenResponse
                    {
                        token = CreateToken(appUser),
                         id=appUser.Id,
                          mail= appUser.Email,
                           name= appUser.Name,
                            surname= appUser.Surname,
                             username = appUser.Username,
                              userRole = appUser.UserRole.ToString()
                    };
                    return tokenResponse;
                }
            } return null;
            

        }

        public async Task LogOut(Guid userId)
        {
            RefreshToken refreshToken = await _unitOfWork.iRefreshTokenRepository.GetRefreshTokenByAppUser(userId);
            refreshToken.isRevoked = true;
            await _unitOfWork.SaveChangesAsync();
        }
        private string GenerateRefreshToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
        , SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

        private string CreateToken(AppUser appUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, appUser.Id.ToString()),
                        new Claim(ClaimTypes.Role, appUser.UserRole.ToString()),
                }),
                Expires = DateTime.Now.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<MessageResponse> CreateAppUser(CreateAppUserRequest appUserRequest)
        {
            AppUser appUser=await _unitOfWork.iappUserRepository.FindByUsername(appUserRequest.username);
            if (appUser != null)
            {
                return null;
            }
            appUser = await _unitOfWork.iappUserRepository.FindByEmail(appUserRequest.email);
            if (appUser!=null)
            {
                return null;
            }
            CreatePasswordHash(appUserRequest.password, out byte[] passwordHash, out byte[] passwordSalt);
            appUser = new AppUser
            {
                Email = appUserRequest.email,
                Name = appUserRequest.name,
                PhoneNumber = appUserRequest.phoneNumber,
                PasswordHash=passwordHash,
                PasswordSalt=passwordSalt,
                Surname = appUserRequest.surname,
                Username = appUserRequest.username,
                UserRole = Role.USER
            };
            await _unitOfWork.iappUserRepository.Add(appUser);
            await _unitOfWork.SaveChangesAsync();
            return new MessageResponse { message="User created successfully." };
        }
        public async Task<AppUser>GetAppUserById(Guid id)
        { 
            return await _unitOfWork.iappUserRepository.GetById(id);
        }
        public async Task<AppUserResponse> GetAppUserResponseById(Guid id)
        {
            AppUser appUser = await _unitOfWork.iappUserRepository.GetById(id);
            return _mapper.Map<AppUserResponse>(appUser);
        }
        public async Task deleteAppUser(Guid id)
        {
            await _unitOfWork.iappUserRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<MessageResponse> editPhoneEmailOrPass(UpdateAppUserRequest updateAppUserRequest, Guid UserId)
        {
            AppUser user = await _unitOfWork.iappUserRepository.GetById(UserId);
            if (updateAppUserRequest.email != null)
            {
                AppUser appUser = await _unitOfWork.iappUserRepository.FindByEmail(updateAppUserRequest.email);
                if (appUser != null)
                {
                    return null;
                }
                user.Email = updateAppUserRequest.email;
            }
            if (updateAppUserRequest.number != null)
            {
                user.PhoneNumber = updateAppUserRequest.number;
            }
            if (updateAppUserRequest.password != null)
            {
                CreatePasswordHash(updateAppUserRequest.password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            _unitOfWork.iappUserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return new MessageResponse { message="User saved"};
        }
    }
}
