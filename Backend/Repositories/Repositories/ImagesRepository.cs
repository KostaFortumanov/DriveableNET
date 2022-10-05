using DAL.Models;
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
    public class ImagesRepository : BaseRepository<Images, ApplicationDbContext>, IImagesRepository
    {
        public ImagesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public  void DeleteByPostId(Guid id)
        {
            _context.images.RemoveRange(_context.images.Where(x => x.Id == id).ToList());
        }
        public  async Task<byte[]> GetFirstImageFromPost(Guid Id)
        {
            Images image = await _context.images.Where(x => x.PostId == Id).FirstOrDefaultAsync();
            return image.Image;
        }
        public async Task<List<byte[]>> GetAllImagesForPost(Guid Id)
        {
            return await _context.images.Where(x => x.PostId == Id).Select(x=>x.Image).ToListAsync();
        }
    }
}
