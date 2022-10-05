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
    public interface IReportPostService
    {
        Task AddReportPost(ReportPost reportPost);
        Task<IEnumerable<AllReportPostsResponse>> GetAllReportPosts();
        Task DeleteReportPost(Guid Id);
        Task DeletePost(Guid Id);
        Task ApprovePost(Guid Id);
        Task<ReportPost> GetReportPostById(Guid Id);
        Task<ReportPost> GetReportPostByPostId(Guid Id);
        Task AddReason(Reason reason, ReportPost reportPost);
        Task<OpenReportPostResponse> GetReportPostResponseByPostId(Guid Id);
    }
}
