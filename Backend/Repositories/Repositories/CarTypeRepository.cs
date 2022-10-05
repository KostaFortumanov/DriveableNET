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
    public class CarTypeRepository : BaseRepository<CarType, ApplicationDbContext>, ICarTypeRepository
    {
        public CarTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<CarType> FindCarByType(string carType)
        {
            return await _context.CarTypes.SingleOrDefaultAsync(z => z.Type == carType);
        }
        public async Task<IEnumerable<string>> GetAllCarTypes()
        {
            return await _context.CarTypes.Select(x => x.Type).ToListAsync();
        }
    }
}
