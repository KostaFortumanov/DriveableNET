using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IReasonService
    {
        Task AddReason(Reason reason);
        Task<Reason> GetReasonById(Guid id);
        void DeleteReasons(ICollection<Reason> reasons);
        Task DeleteReason(Guid Id);
    }
}
