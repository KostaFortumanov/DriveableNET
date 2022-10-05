using DAL.Dto.Request;
using DAL.Models;
using DAL.Models.Relations;
using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class PostRepository : BaseRepository<Post, ApplicationDbContext>, IPostRepository
    {
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Post>> GetAllInclude()
        {
            return await _context.Posts.Include(z => z.Creator).Include(z => z.PostCarTypes).Include(z => z.car).ToListAsync();
        }
        public async Task<Post> GetByIdInclude(Guid id)
        {
            return await _context.Posts.Include(z => z.Creator).Include(z => z.PostCarTypes).Include(z => z.car).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<IEnumerable<Post>> GetAllPostsByAppUserId(Guid AppUserId)
        {
            return await _context.Posts.Where(z => z.CreatorId == AppUserId).ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetAllPostsPaginated(int page, int pageSize,List<CarType>cartypes)
        {
            var skip = page == 1 ? 0 : pageSize * (page-1);
            var PostsIsNew=_context.Posts.Where(z => z.IsNew == true);
            string manufacturer = "Fiat";
            
            if (!String.IsNullOrEmpty(manufacturer))
            {
                PostsIsNew = PostsIsNew.Where(z => z.car.Manufacturer == manufacturer);
            } CarType c = new CarType { Type = "ok" };
                PostsIsNew = PostsIsNew.Include(z=>z.PostCarTypes).ThenInclude(z=>z.CarType).Where(z =>z.PostCarTypes.Any(x=>cartypes.Contains(x.CarType)));
            
            
            return await PostsIsNew.OrderBy(z=>z.Date).Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllSorted(GetPostsRequest postsRequest,List<CarType> carTypes)
        {
            var Posts=_context.Posts.AsQueryable();
            if (postsRequest.isNew != null)
            {
                Posts =Posts.Where(z => z.IsNew == postsRequest.isNew);
            }
            if (postsRequest.color!=null)
            {
                Posts = Posts.Where(z => z.Color == postsRequest.color.ToUpper());
            }
            if (postsRequest.priceFrom != null)
            {
                Posts = Posts.Where(z => z.Price >= postsRequest.priceFrom);
            }
            if (postsRequest.priceTo != null)
            {
                Posts = Posts.Where(z => z.Price <= postsRequest.priceTo);
            }
            if (postsRequest.yearFrom != null)
            {
                Posts = Posts.Where(z => z.ManufacturingYear >= postsRequest.yearFrom);
            }
            if (postsRequest.yearTo != null)
            {
                Posts = Posts.Where(z => z.ManufacturingYear <= postsRequest.yearTo);
            }
            if (postsRequest.mileageBelow != null)
            {
                Posts = Posts.Where(z => z.Mileage <= postsRequest.mileageBelow);
            }
            Posts = Posts.Include(z => z.car);
            if (postsRequest.manufacturer!= null)
            {
                Posts = Posts.Where(z => z.car.Manufacturer == postsRequest.manufacturer);
            }
            if (postsRequest.model != null)
            {
                Posts = Posts.Where(z => z.car.Model == postsRequest.model);
            }
            Posts =Posts.Include(z => z.Creator);
            if (carTypes.Any())
            {
                Posts = Posts.Include(z => z.PostCarTypes).ThenInclude(z => z.CarType).Where(z => z.PostCarTypes.Any(x => carTypes.Contains(x.CarType)));
            }
            if (postsRequest.sortBy!=null)
            {
                if (postsRequest.sortBy.ToLower().Equals("date"))
                {
                    Posts = Posts.OrderByDescending(z => z.Date);
                }
                if (postsRequest.sortBy.ToLower().Equals("price"))
                {
                    Posts = Posts.OrderBy(z => z.Price);
                }
            }
            if (postsRequest.Size!=null)
            {
                if (postsRequest.Page!=null)
                {
                    return await Posts.Skip(postsRequest.Size.Value * (postsRequest.Page.Value - 1)).Take(postsRequest.Size.Value).ToListAsync();
                }
                return await Posts.Skip(0).Take(postsRequest.Size.Value).ToListAsync();
            }
            return await Posts.ToListAsync();
        }
    }
}
