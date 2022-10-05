using DAL.Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICarTypeRepository : IBaseRepository<CarType>
    {
        Task<CarType> FindCarByType(string carType);
        Task<IEnumerable<string>> GetAllCarTypes();
    }
}
