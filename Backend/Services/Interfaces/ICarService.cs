using DAL.Dto.Request;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICarService
    {
        public Task<Car> AddCar(AddCarRequest carRequest);
        public Task<IEnumerable<Car>> GetAll();
        public Task<Car> GetCarById(Guid id);
        public Task DeleteCar(Guid id);
        public Task<IEnumerable<String>> GetAllManufacturers();
        public Task<IEnumerable<String>> GetAllModelsByManufacturer(string Manufacturer);
    }
}
