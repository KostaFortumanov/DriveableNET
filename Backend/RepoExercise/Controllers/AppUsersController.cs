using DAL.Dto.Request;
using DAL.Dto.Response;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Security.Claims;

namespace RepoExercise.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(Roles="USER")]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUserService _iappUserService;
        private readonly IPostService _ipostService;
        public AppUsersController(IAppUserService iappUserService, IPostService ipostService)
        {
            _iappUserService = iappUserService;
            _ipostService = ipostService;
        }

        [HttpPost("edit/phoneNumber")]
        public async Task<IActionResult> editPhoneNumber(UpdateAppUserRequest updateAppUserRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var response = await _iappUserService.editPhoneEmailOrPass(updateAppUserRequest, Guid.Parse(identity.Name));
            if (response!=null)
            {
                return Ok(response);
            }
            return BadRequest("This Email is not available.");
        }
        [HttpPost("edit/email")]
        public async Task<IActionResult> editPassword(UpdateAppUserRequest updateAppUserRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity; 
            var response = await _iappUserService.editPhoneEmailOrPass(updateAppUserRequest, Guid.Parse(identity.Name));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest("This Email is not available.");
        }
        [HttpPost("edit/password")]
        public async Task<IActionResult> editEmail(UpdateAppUserRequest updateAppUserRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var response = await _iappUserService.editPhoneEmailOrPass(updateAppUserRequest, Guid.Parse(identity.Name));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest("This Email is not available.");
        }
        [HttpGet("posts")]
        public async Task<IActionResult> GetAllPostsByAppUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return Ok(await _ipostService.GetAllPostsByAppUserId(Guid.Parse(identity.Name)));
        }
        [HttpPost("edit/price/{id}")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> ChangePriceOnPost([FromRoute] Guid id,ChangePriceOnPostRequest priceOnPostRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            Post post = await _ipostService.GetPostById(id);
            if (post.CreatorId.CompareTo(Guid.Parse(identity.Name)) == 0)
            {
                return Ok(await _ipostService.ChangePriceOnPost(priceOnPostRequest, id));
            }
            return BadRequest("You can't change the price on the posts that you didn't create.");
        }
    }
}
