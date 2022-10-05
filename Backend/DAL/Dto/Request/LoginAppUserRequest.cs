using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto.Request
{
    public class LoginAppUserRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
