using DAL.Dto.Request;
using DAL.Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllPostsByAppUserId(Guid AppUserId);
        Task<IEnumerable<Post>> GetAllInclude();
        Task<Post> GetByIdInclude(Guid id);
        Task<IEnumerable<Post>> GetAllSorted(GetPostsRequest postsRequest, List<CarType> carTypes);
    }
}
