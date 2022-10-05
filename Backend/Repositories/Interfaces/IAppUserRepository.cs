using DAL.Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IAppUserRepository:IBaseRepository<AppUser>
    {
        public Task<dynamic> get();
        Task<AppUser> FindByUsername(string username);
        Task<AppUser> FindByEmail(string mail);
    }
}
