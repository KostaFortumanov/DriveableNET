using DAL.Dto.Request;
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
    public class ImagesService : IImagesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImagesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddImage(Images image)
        {
            await _unitOfWork.iimagesRepository.Add(image);
        }

        public async Task DeleteByPostID(Guid postID)
        {
            _unitOfWork.iimagesRepository.DeleteByPostId(postID);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<string> GetExampleImage(getImageRequest request)
        {
            var images = await _unitOfWork.iimagesRepository.GetAllImagesForPost(request.Id);
            var image = images.ElementAt(request.image);
            return Convert.ToBase64String(image);
        }
    }
}
