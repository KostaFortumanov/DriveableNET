using DAL.Dto.Request;
using DAL.Dto.Response;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Security.Claims;

namespace RepoExercise.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _ipostService;
        private readonly ICarService _icarService;
        private readonly IImagesService _iimagesService;
        private readonly IReportPostService _ireportPostService;
        private readonly IReasonService _ireasonService;
        private readonly ICarTypeService _icarTypeService;
        private readonly IAppUserService _iappUserService;
        public PostsController(IPostService postService, ICarService _carService, IImagesService imagesService, IReportPostService ireportPostService, IReasonService ireasonService, ICarTypeService _carTypeService, IAppUserService iappUserService)
        {
            _ipostService = postService;
            _icarService = _carService;
            _iimagesService = imagesService;
            _ireportPostService = ireportPostService;
            _ireasonService = ireasonService;
            _icarTypeService = _carTypeService;
            _iappUserService = iappUserService;
        }

        [HttpPost("create")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> AddPost([FromBody] AddPostRequest postRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return Ok(await _ipostService.AddPost(postRequest,Guid.Parse(identity.Name)));
        }
        [HttpDelete]
        [Authorize(Roles="USER")]
        public async Task<IActionResult> DeletePost(Guid Id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            Post post =await _ipostService.GetPostById(Id);
            if (post.CreatorId.CompareTo(Guid.Parse(identity.Name))==0)
            {
                ReportPost repPost = await _ireportPostService.GetReportPostByPostId(Id);
                if (repPost != null)
                {
                    _ireasonService.DeleteReasons(repPost.Reasons);
                }
                await _ipostService.DeletePost(Id);
                return Ok(Id + " has been deleted.");
            }
            return BadRequest("You can't delete posts that you have not created");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ipostService.GetAll(null));
        }
        [HttpPost]
        public async Task<IActionResult> GetAllPosts([FromBody] GetPostsRequest postsRequest)
        {
            (var postsResponse, var size) = await _ipostService.GetAll(postsRequest);
            return Ok(new { postsResponse, size });
        }
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetAllPostsByAppUserId([FromRoute] Guid id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return Ok(await _ipostService.GetAllPostsByAppUserId(id));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            return Ok(await _ipostService.GetPostResponseById(id));
        }
        [HttpPost("{id}/report")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> AddReason([FromRoute] Guid id,[FromBody] AddReportPost reportPostRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            AppUser Creator = await _iappUserService.GetAppUserById(Guid.Parse(identity.Name));
            Post post = await _ipostService.GetPostById(id);
            Reason reason = new Reason
            {
                Description = reportPostRequest.description,
                appUser = Creator
            };
            await _ireasonService.AddReason(reason);
            reason = await _ireasonService.GetReasonById(reason.Id);
            ReportPost report = await _ireportPostService.GetReportPostByPostId(id);
            if (report == null)
            {
                report = new ReportPost
                {
                    post = post,
                    Reasons = new Collection<Reason>()
                };
                report.Reasons.Add(reason);
                await _ireportPostService.AddReportPost(report);
            }
            else
            {
                await _ireportPostService.AddReason(reason, report);
            }
            return Ok(new MessageResponse { message = "Report added." });
        }
        [HttpGet("image/{id}/{image}")]
        public async Task<IActionResult> GetImage([FromRoute] Guid id, [FromRoute] int image)
        {
            return Ok(await _iimagesService.GetExampleImage(new getImageRequest {image=image, Id=id }));
        }
        [HttpGet("manufacturer")]
        public async Task<IActionResult> GetAllManufacturers()
        {
            return Ok(await _icarService.GetAllManufacturers());
        }
        [HttpGet("model")]
        public async Task<IActionResult> GetAllModelsByManufacturer([FromQuery] string Manufacturer)
        {
            return Ok(await _icarService.GetAllModelsByManufacturer(Manufacturer));
        }
        [HttpGet("carTypes")]
        public async Task<IActionResult> GetAllCarTypes()
        {
            return Ok(await _icarTypeService.GetAllCarTypes());
        }


    }
}
