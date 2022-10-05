using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto.Request
{
    public class UpdateAppUserRequest
    {
        public string? email { get; set; }
        public string? number { get; set; }
        public string? password { get; set; }
    }
}
