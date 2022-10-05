using DAL.Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IReasonRepository : IBaseRepository<Reason>
    {
        void DeleteReasons(ICollection<Reason> reasons);
        Task LoadAppUser(Reason reason);
    }
}
