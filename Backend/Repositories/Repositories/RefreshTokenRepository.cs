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
    public class RefreshTokenRepository : BaseRepository<RefreshToken, ApplicationDbContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<RefreshToken> GetRefreshTokenByToken(string token)
        {
            return await _context.RefreshTokens.Where(x => x.refreshToken == token).Include(x=>x.appUser).FirstOrDefaultAsync();
        }
        public async Task<RefreshToken> GetRefreshTokenByAppUser(Guid userId)
        {
            return await _context.RefreshTokens.Where(x => x.AppUserId == userId).Include(x => x.appUser).FirstOrDefaultAsync();
        }
    }
}
