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
    public class AppUserRepository : BaseRepository<AppUser, ApplicationDbContext>, IAppUserRepository
    {
        public AppUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<dynamic> get()
        {
            return new
            {
                name = "Nikola"
            };
        }
        public async Task<AppUser> FindByUsername(string username)
        {
            return await _context.AppUsers.SingleOrDefaultAsync(x => x.Username == username);
        }
        public async Task<AppUser> FindByEmail(string mail)
        {
            return await _context.AppUsers.SingleOrDefaultAsync(x => x.Email == mail);
        }

    }
}
