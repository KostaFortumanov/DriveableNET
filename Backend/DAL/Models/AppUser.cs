using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public enum Role
    {
        USER,
        MODERATOR
    }
    public class AppUser
    {
        public Guid Id { get; set; }
        [Required]
        public Role UserRole { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
