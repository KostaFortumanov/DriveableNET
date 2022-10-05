using DAL.Dto.Request;
using DAL.Dto.Response;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAppUserService
    {
        Task<MessageResponse> CreateAppUser(CreateAppUserRequest appUserRequest);
        Task<IEnumerable<AppUserResponse>> getall();
        public Task<AppUser> GetAppUserById(Guid id);
        Task<AppUserResponse> GetAppUserResponseById(Guid id);
        public Task deleteAppUser(Guid id);
        Task<MessageResponse> editPhoneEmailOrPass(UpdateAppUserRequest updateAppUserRequest, Guid UserId);
        Task<TokenResponse?> login(LoginAppUserRequest loginRequest);
        Task LogOut(Guid userId);
    }
}
