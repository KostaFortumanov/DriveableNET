using DAL.Dto.Request;
using DAL.Models;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CarTypeService : ICarTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CarType> AddCarType(AddCarTypeRequest carTypeRequest)
        {
            CarType carType = new CarType()
            {
                Type = carTypeRequest.Type
            };
            await _unitOfWork.icarTypeRepository.Add(carType);
            await _unitOfWork.SaveChangesAsync();
            return carType;
        }

        public async Task DeleteCarType(Guid id)
        {
            await _unitOfWork.icarTypeRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarType>> GetAll()
        {
            return await _unitOfWork.icarTypeRepository.GetAll();
        }

        public async Task<CarType> GetCarTypeById(Guid id)
        {
            return await _unitOfWork.icarTypeRepository.GetById(id);
        }
        public async Task<IEnumerable<string>> GetAllCarTypes()
        {
            return await _unitOfWork.icarTypeRepository.GetAllCarTypes();
        }
    }
}
