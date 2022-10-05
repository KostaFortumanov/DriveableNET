using DAL.Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
       Task<IEnumerable<string>> GetAllManufacturers();
       Task<IEnumerable<string>> GetAllModelsByManufacturer(string manufacturer);
       Task<Car> GetCarByManufacturerAndModel(string Manufacturer, string Model);
    }
}
