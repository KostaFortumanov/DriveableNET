using DAL.Models;
using Repositories.Base;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ReasonRepository : BaseRepository<Reason, ApplicationDbContext>, IReasonRepository
    {
        public ReasonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public void DeleteReasons(ICollection<Reason> reasons)
        {
             _context.Reasons.RemoveRange(reasons);
        }
        public async Task LoadAppUser(Reason reason)
        {
            await _context.Entry(reason).Reference(s => s.appUser).LoadAsync();
        }

    }
}
