using DAL.Dto.Request;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICarTypeService
    {
        public Task<CarType> AddCarType(AddCarTypeRequest carTypeRequest);
        public Task<IEnumerable<CarType>> GetAll();
        public Task<CarType> GetCarTypeById(Guid id);
        public Task DeleteCarType(Guid id);
        Task<IEnumerable<string>> GetAllCarTypes();
    }
}
