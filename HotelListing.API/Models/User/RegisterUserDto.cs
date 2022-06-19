using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.User
{
    public class RegisterUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(15,ErrorMessage ="Your Password is limited from {2} to {1} characters",MinimumLength =6)]
        public string Password { get; set; }
        public bool isAdmin { get; set; }
    }
}
