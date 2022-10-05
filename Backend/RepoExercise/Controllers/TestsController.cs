using DAL.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Security.Claims;

namespace RepoExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IAppUserService _iappUserService;
        private readonly IReasonService _ireasonService;
        private readonly IReportPostService _ireportPostService;

        public TestsController(IAppUserService iappUserService,IReasonService ireasonService, IReportPostService ireportPostService)
        {
            _iappUserService = iappUserService;
            _ireasonService= ireasonService;
            _ireportPostService = ireportPostService;
        }
        [HttpGet]
        [Authorize(Roles ="MODERATOR")]
        public async Task<IActionResult> getallusers()
        {
          return Ok(await _iappUserService.getall());
        }
        [HttpGet("{Id}")]
        [Authorize(Roles = "MODERATOR")]
        public async Task<IActionResult> GetAppUserById([FromRoute]Guid Id)
        {
            return Ok(await _iappUserService.GetAppUserResponseById(Id));
        }
        [HttpDelete("DeleteReason")]
        [Authorize(Roles ="MODERATOR")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ireasonService.DeleteReason(id);
            return Ok();
        }
        [HttpDelete("DeleteReportPost")]
        [Authorize(Roles = "MODERATOR")]
        public async Task<IActionResult> DeleteReportPost(Guid id)
        {
            await _ireportPostService.DeleteReportPost(id);
            return Ok();
        }
    }
}
