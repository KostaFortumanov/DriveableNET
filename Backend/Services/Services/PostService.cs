using AutoMapper;
using DAL.Dto.Request;
using DAL.Dto.Response;
using DAL.Models;
using DAL.Models.Relations;
using Microsoft.AspNetCore.Http;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICarService _carService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        public PostService(IUnitOfWork unitOfWork, ICarService carService, IAppUserService appUserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _carService = carService;
            _appUserService = appUserService;
            _mapper = mapper;
        }

        public async Task<OpenPostResponse> AddPost(AddPostRequest PostRequest,Guid CreatorId)
        {
            Car car1 = await _unitOfWork.icarRepository.GetCarByManufacturerAndModel(PostRequest.manufacturer, PostRequest.model);
            if (await _unitOfWork.icarRepository.GetCarByManufacturerAndModel(PostRequest.manufacturer,PostRequest.model)==null)
            {
                car1 = new Car
                {
                    Manufacturer = PostRequest.manufacturer,
                    Model = PostRequest.model
                };
            }
            Post post = new Post
            {
                car = car1,
                Color = PostRequest.color.ToUpper(),
                Creator = _appUserService.GetAppUserById(CreatorId).Result,
                Date = DateTime.Now,
                Description = PostRequest.description,
                Horsepower = PostRequest.horsepower,
                IsNew = PostRequest.isNew,
                ManufacturingYear = PostRequest.manufacturingYear,
                Mileage = PostRequest.mileage,
                Price = PostRequest.price,
                Title = PostRequest.title
            };
            await _unitOfWork.ipostRepository.Add(post);
            foreach (string img in PostRequest.images)
            {
                    byte[] arr = Encoding.UTF8.GetBytes(img);
                    Images images = new Images
                    {
                        Image = arr,
                        post = post
                    };
                    await _unitOfWork.iimagesRepository.Add(images);

            }
            foreach(string cartype in PostRequest.carType)
            {
                CarType carType=await _unitOfWork.icarTypeRepository.FindCarByType(cartype.ToUpper());
                PostCarType postCarType = new PostCarType
                {
                    CarType = carType,
                    post = post
                };
                await _unitOfWork.ipostCarTypeRepository.Add(postCarType);
            }
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OpenPostResponse>(post);
        }

        public async Task<(IEnumerable<PostResponse>, int size)> GetAll(GetPostsRequest postsRequest)
        {
            IEnumerable<Post> postresp2;
            var size = 3;
            IEnumerable<PostResponse> postResponses = null;
            if (postsRequest != null)
            {
                List<CarType> carTypes = new List<CarType>();
                if (postsRequest.carTypes != null)
                {
                    foreach (string c in postsRequest.carTypes)
                    {
                        carTypes.Add(await _unitOfWork.icarTypeRepository.FindCarByType(c.ToUpper()));
                    }
                }
                postResponses = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResponse>>(await _unitOfWork.ipostRepository.GetAllSorted(postsRequest, carTypes));
                postsRequest.Size = null;
                postresp2 = await _unitOfWork.ipostRepository.GetAllSorted(postsRequest, carTypes);
                size = postresp2.Count();
            }
            else
            {
                postresp2 = await _unitOfWork.ipostRepository.GetAll();
                size = postresp2.Count();
                postResponses = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResponse>>(await _unitOfWork.ipostRepository.GetAll());
            }
            foreach (PostResponse p in postResponses)
            {
                var images = await _unitOfWork.iimagesRepository.GetAllImagesForPost(p.id);
                p.numOfImages = images.Count;
                p.carType = await _unitOfWork.ipostCarTypeRepository.GetAllCarTypesByPost(p.id);
            }

            return (postResponses, size);
        }
        public async Task<Post> GetPostById(Guid PostId)
        {
            return await _unitOfWork.ipostRepository.GetById(PostId);
        }
        public async Task<OpenPostResponse> GetPostResponseById(Guid PostId)
        {
            var images = await _unitOfWork.iimagesRepository.GetAllImagesForPost(PostId);
            OpenPostResponse postResponse = _mapper.Map<OpenPostResponse>(await _unitOfWork.ipostRepository.GetByIdInclude(PostId));
            postResponse.numOfImages = images.Count;
            postResponse.carType = await _unitOfWork.ipostCarTypeRepository.GetAllCarTypesByPost(PostId);
            return postResponse;
        }
        public async Task<MessageResponse> ChangePriceOnPost(ChangePriceOnPostRequest priceOnPostRequest, Guid id)
        {
            Post post=await _unitOfWork.ipostRepository.GetByIdInclude(id);
            post.Price = priceOnPostRequest.price;
            _unitOfWork.ipostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return new MessageResponse { message = "Price changed" };
        }
        public async Task<ExamplePostResponse> GetExamplePostResponse()
        {
            IEnumerable<Post> posts = await _unitOfWork.ipostRepository.GetAll();
            Post post = posts.First();
            return _mapper.Map<ExamplePostResponse>(post);
        }

        public async Task DeletePost(Guid Id)
        {
            await _unitOfWork.ipostRepository.Delete(Id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<PostResponse>> GetAllPostsByAppUserId(Guid Id)
        {
            return _mapper.Map<IEnumerable<Post>, IEnumerable<PostResponse>>(await _unitOfWork.ipostRepository.GetAllPostsByAppUserId(Id));
        }
    }
}
