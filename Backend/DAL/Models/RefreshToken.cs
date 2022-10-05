using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string refreshToken { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser appUser { get; set; }
        public bool isRevoked { get; set; }
    }
}
