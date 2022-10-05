using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto.Response
{
    public class TokenResponse
    {
        public string token { get; set; }
        public Guid id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string mail { get; set; }
        public string userRole { get; set; }
    }
}
