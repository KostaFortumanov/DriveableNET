using DAL.Dto.Request;
using DAL.Dto.Response;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace RepoExercise.Controllers
{
    [Route("api/moderator")]
    [ApiController]
    [Authorize(Roles ="MODERATOR")]
    public class ModeratorController : ControllerBase
    {
        private readonly IReasonService _ireasonService;
        private readonly IReportPostService _ireportPostService;

        public ModeratorController(IReportPostService ireportPostService, IReasonService ireasonService)
        {
            _ireportPostService = ireportPostService;
            _ireasonService = ireasonService;
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ireportPostService.GetAllReportPosts());
        }
        [HttpGet("post/{Id}")]
        public async Task<IActionResult> GetReportPostByPostId(Guid Id)
        {
            return Ok(await _ireportPostService.GetReportPostResponseByPostId(Id));
        }
        [HttpGet("post/{id}/approve")]
        public async Task<IActionResult> ApprovePost([FromRoute]Guid id)
        {
            ReportPost repPost = await _ireportPostService.GetReportPostByPostId(id);
            _ireasonService.DeleteReasons(repPost.Reasons);
            await _ireportPostService.ApprovePost(id);
            return Ok(new MessageResponse { message = "Post approved." });
        }
        [HttpGet("post/{id}/delete")]
        public async Task<IActionResult> DissaprovePost([FromRoute]Guid id)
        {
            ReportPost repPost = await _ireportPostService.GetReportPostByPostId(id);
            _ireasonService.DeleteReasons(repPost.Reasons);
            await _ireportPostService.DeletePost(id);
            return Ok(new MessageResponse { message = "Post deleted." });
        }
    }
}
