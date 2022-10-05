using DAL.Dto.Request;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IImagesService
    {
        Task AddImage(Images image);
        Task DeleteByPostID(Guid postID);
        Task<string> GetExampleImage(getImageRequest request);
    }
}
