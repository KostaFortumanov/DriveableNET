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
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Car> AddCar(AddCarRequest carRequest)
        {
            Car car = new Car
            {
                 Manufacturer = carRequest.Manufacturer,
                  Model = carRequest.Model
            };
            await _unitOfWork.icarRepository.Add(car);
            await _unitOfWork.SaveChangesAsync();
            return car;
        }

        public async Task DeleteCar(Guid id)
        {
            await _unitOfWork.icarRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _unitOfWork.icarRepository.GetAll();
        }

        public async Task<Car> GetCarById(Guid id)
        {
            return await _unitOfWork.icarRepository.GetById(id);
        }
        public async Task<IEnumerable<String>> GetAllManufacturers()
        {
            return await _unitOfWork.icarRepository.GetAllManufacturers();
        }
        public async Task<IEnumerable<String>> GetAllModelsByManufacturer(string Manufacturer)
        {
            return await _unitOfWork.icarRepository.GetAllModelsByManufacturer(Manufacturer);
        }

    }
}
