using System.ComponentModel.DataAnnotations;

namespace Services.AuthAPI.Shared.Models.Dto.User
{
    public class NewUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; } // remove it if using AD
    }
}
