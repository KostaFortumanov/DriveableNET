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
    public class ReasonService : IReasonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReasonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddReason(Reason reason)
        {
            await _unitOfWork.ireasonRepository.Add(reason);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<Reason> GetReasonById (Guid id)
        {
        return await _unitOfWork.ireasonRepository.GetById(id);
        }
        public async Task DeleteReason(Guid Id)
        {
            await _unitOfWork.ireasonRepository.Delete(Id);
            await _unitOfWork.SaveChangesAsync();
        }
        public void DeleteReasons(ICollection<Reason> reasons)
        {
             _unitOfWork.ireasonRepository.DeleteReasons(reasons);
        }
    }
}
