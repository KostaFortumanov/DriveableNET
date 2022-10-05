using AutoMapper;
using DAL.Dto.Request;
using DAL.Dto.Response;
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
    public class ReportPostService : IReportPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportPostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddReportPost(ReportPost reportPost)
        {
           await _unitOfWork.ireportPostRepository.Add(reportPost);
           await _unitOfWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<AllReportPostsResponse>> GetAllReportPosts()
        {
           IEnumerable<ReportPost> reportPosts = await _unitOfWork.ireportPostRepository.GetAllInclude(null,null);
           IEnumerable<AllReportPostsResponse> reportPostsResponses = _mapper.Map<IEnumerable<ReportPost>, IEnumerable<AllReportPostsResponse>>(reportPosts);
           foreach(AllReportPostsResponse rep in reportPostsResponses)
            {
                
                var Images = await _unitOfWork.iimagesRepository.GetAllImagesForPost(rep.id);
                rep.numOfImages = Images.Count;
                rep.carType = await _unitOfWork.ipostCarTypeRepository.GetAllCarTypesByPost(rep.id);
            }
            return reportPostsResponses;
        }
        public async Task DeleteReportPost(Guid Id)
        {
            await _unitOfWork.ireportPostRepository.Delete(Id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ApprovePost(Guid Id)
        {
            await _unitOfWork.ireportPostRepository.ApprovePost(Id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeletePost(Guid Id)
        {
            await _unitOfWork.ipostRepository.Delete(Id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<ReportPost> GetReportPostById(Guid Id)
        {
            return await _unitOfWork.ireportPostRepository.GetById(Id);
        }

        public async Task<OpenReportPostResponse> GetReportPostResponseByPostId(Guid Id)
        {
            ReportPost repPost = await _unitOfWork.ireportPostRepository.GetReportPostByIdInclude(Id);
            if (repPost!=null)
            {
                var images = await _unitOfWork.iimagesRepository.GetAllImagesForPost(Id);
                OpenReportPostResponse post = _mapper.Map<OpenReportPostResponse>(repPost);
                post.numOfImages = images.Count;
                post.carType = await _unitOfWork.ipostCarTypeRepository.GetAllCarTypesByPost(Id);
                foreach (Reason reason in repPost.Reasons)
                {
                    await _unitOfWork.ireasonRepository.LoadAppUser(reason);
                }
                post.reasons = _mapper.Map<IEnumerable<Reason>, IEnumerable<ReasonResponse>>(repPost.Reasons);
                return post;
            }
            return null;
        }
        public Task<ReportPost> GetReportPostByPostId(Guid Id)
        {
            return _unitOfWork.ireportPostRepository.GetByPostId(Id);
        }
        public async Task AddReason(Reason reason,ReportPost reportPost)
        {
            reportPost.Reasons.Add(reason);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
