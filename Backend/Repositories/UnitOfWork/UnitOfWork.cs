using DAL.Models;
using Repositories.Interfaces;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UnitOfWork
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private AppUserRepository _appUserRepository;
        private CarRepository _carRepository;
        private CarTypeRepository _carTypeRepository;
        private ImagesRepository _imagesRepository;
        private PostCarTypeRepository _postCarTypeRepository;
        private PostRepository _postRepository;
        private ReasonRepository _reasonRepository;
        private ReportPostRepository _reportPostRepository;
        private RefreshTokenRepository _refreshTokenRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IAppUserRepository iappUserRepository
        {
            get { return _appUserRepository ?? (_appUserRepository = new AppUserRepository(_context)); }
        }
        public ICarRepository icarRepository
        {
            get { return _carRepository ?? (_carRepository = new CarRepository(_context)); }
        }
        public ICarTypeRepository icarTypeRepository
        {
            get { return _carTypeRepository ?? (_carTypeRepository = new CarTypeRepository(_context)); }
        }
        public IImagesRepository iimagesRepository
        {
            get { return _imagesRepository ?? (_imagesRepository = new ImagesRepository(_context)); }
        }
        public IPostCarTypeRepository ipostCarTypeRepository
        {
            get { return _postCarTypeRepository ?? (_postCarTypeRepository = new PostCarTypeRepository(_context)); }
        }
        public IPostRepository ipostRepository
        {
            get { return _postRepository ?? (_postRepository = new PostRepository(_context)); }
        }
        public IReasonRepository ireasonRepository
        {
            get { return _reasonRepository ?? (_reasonRepository = new ReasonRepository(_context)); }
        }
        public IReportPostRepository ireportPostRepository
        {
            get { return _reportPostRepository ?? (_reportPostRepository = new ReportPostRepository(_context)); }
        }
        public IRefreshTokenRepository iRefreshTokenRepository
        {
            get { return _refreshTokenRepository ?? (_refreshTokenRepository = new RefreshTokenRepository(_context)); }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
