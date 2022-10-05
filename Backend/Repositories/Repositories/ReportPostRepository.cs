using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ReportPostRepository : BaseRepository<ReportPost, ApplicationDbContext>, IReportPostRepository
    {
        public ReportPostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task ApprovePost(Guid Id)
        {
            ReportPost reportPost = await _context.ReportPosts.Include(z=>z.Reasons).SingleOrDefaultAsync(z => z.PostId == Id);
            _context.Remove(reportPost);
        }

        public async Task<ReportPost> GetByPostId(Guid Id)
        {
            return await _context.ReportPosts.Include(z=>z.Reasons).SingleOrDefaultAsync(z => z.PostId == Id);
        }
        public async Task<ReportPost> GetReportPostByIdInclude(Guid Id)
        {
            return await _context.ReportPosts.Include(z => z.Reasons).Include(z=>z.post).Include(z=>z.post.Creator).Include(z=>z.post.car).SingleOrDefaultAsync(z => z.PostId == Id);
        }
        public async Task<IEnumerable<ReportPost>> GetAllInclude(int? page,int? size)
        {
            if (size!=null)
            {
                if (page!=null)
                {
                    return await _context.ReportPosts.Include(z => z.post).Include(z => z.post.Creator).Include(z => z.post.car).Skip((page.Value - 1) * size.Value).Take(size.Value).ToListAsync();
                }
                return await _context.ReportPosts.Include(z => z.post).Include(z => z.post.Creator).Include(z => z.post.car).Skip(0).Take(size.Value).ToListAsync();
            }
            return await _context.ReportPosts.Include(z => z.post).Include(z => z.post.Creator).Include(z => z.post.car).ToListAsync();
        }
    }
}
