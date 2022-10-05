using DAL.Models.Relations;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPostCarTypeRepository : IBaseRepository<PostCarType>
    {
        Task<IEnumerable<string>> GetAllCarTypesByPost(Guid PostId);
    }
}
