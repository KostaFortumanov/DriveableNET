using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace DAL.Dto.Request
{
    public class CreateAppUserRequest
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string surname { get; set; }
        [Required]
        public string phoneNumber { get; set; }
    }
}
