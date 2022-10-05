using DAL.Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IImagesRepository : IBaseRepository<Images>
    {
        public void DeleteByPostId(Guid id);
        Task<byte[]> GetFirstImageFromPost(Guid Id);
        Task<List<byte[]>> GetAllImagesForPost(Guid Id);
    }
}
