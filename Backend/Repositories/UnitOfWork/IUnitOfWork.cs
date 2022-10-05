using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAppUserRepository iappUserRepository
        {
            get;
        }
        IRefreshTokenRepository iRefreshTokenRepository
        {
            get;
        }
        ICarRepository icarRepository
        {
            get;
        }
        ICarTypeRepository icarTypeRepository
        {
            get;
        }
        IImagesRepository iimagesRepository
        {
            get;
        }
        IPostCarTypeRepository ipostCarTypeRepository
        {
            get;
        }
        IPostRepository ipostRepository
        {
            get;
        }
        IReasonRepository ireasonRepository
        {
            get;
        }
        IReportPostRepository ireportPostRepository
        {
            get;
        }
        public Task SaveChangesAsync();
        public void Commit();
    }
    
}
