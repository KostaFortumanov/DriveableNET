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
    public class CarRepository : BaseRepository<Car, ApplicationDbContext>, ICarRepository
    {
        public CarRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Car> GetCarByManufacturerAndModel(string Manufacturer,string Model)
        {
            return await _context.Cars.Where(z => z.Manufacturer == Manufacturer && z.Model==Model).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<string>> GetAllManufacturers()
        {
            return  await _context.Cars.Select(x => x.Manufacturer).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllModelsByManufacturer(string manufacturer)
        {
            return await _context.Cars.Where(x=>x.Manufacturer==manufacturer).Select(x=>x.Model).Distinct().ToListAsync();
        }
    }
}
