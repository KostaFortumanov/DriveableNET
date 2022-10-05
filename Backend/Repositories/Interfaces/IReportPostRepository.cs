using DAL.Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IReportPostRepository : IBaseRepository<ReportPost>
    {
        Task ApprovePost(Guid Id);
        Task<ReportPost> GetByPostId(Guid Id);
        Task<ReportPost> GetReportPostByIdInclude(Guid Id);
        Task<IEnumerable<ReportPost>> GetAllInclude(int? page, int? size);
    }
}
