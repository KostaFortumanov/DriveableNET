using DAL.Models;
using DAL.Models.Relations;
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
    public class PostCarTypeRepository : BaseRepository<PostCarType, ApplicationDbContext>, IPostCarTypeRepository
    {
        public PostCarTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<string>> GetAllCarTypesByPost(Guid PostId)
        {
            return await _context.PostCarTypes.Where(x => x.PostId == PostId).Include(x => x.CarType).Select(x => x.CarType.Type).ToListAsync();
        }
    }
}
