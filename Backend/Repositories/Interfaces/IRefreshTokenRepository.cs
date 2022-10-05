using DAL.Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IRefreshTokenRepository:IBaseRepository<RefreshToken>
    {
        Task<RefreshToken> GetRefreshTokenByToken(string token);
        Task<RefreshToken> GetRefreshTokenByAppUser(Guid userId);
    }
}
