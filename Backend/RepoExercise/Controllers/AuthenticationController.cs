using DAL.Dto.Request;
using DAL.Dto.Response;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Security.Claims;

namespace RepoExercise.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAppUserService _iappUserService;

        public AuthenticationController(IAppUserService appUserService)
        {
            _iappUserService = appUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> createappuser([FromBody] CreateAppUserRequest appUserRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity.Name==null)
            {
                MessageResponse? response = await _iappUserService.CreateAppUser(appUserRequest);
            if (response==null)
            {
                return BadRequest("User already exists.");
            }
            return Ok(response);
            }
            else
            {
                return BadRequest("You can't create new account while logged in.");
            }
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> login(LoginAppUserRequest loginRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity.Name == null)
            {
                TokenResponse? tokenresponse=await _iappUserService.login(loginRequest);
                if (tokenresponse != null)
                {
                    return Ok(tokenresponse);
                }
                return BadRequest(new MessageResponse { message = "Invalid login credentials." });
            }
            else
            {
                return BadRequest("You are already logged in.");
            }
        }

        [HttpGet("logOut")]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity.Name != null)
            {
                await _iappUserService.LogOut(Guid.Parse(identity.Name));
                return Ok();
            }
            return BadRequest("You are not logged in.");
        }
    }
}
