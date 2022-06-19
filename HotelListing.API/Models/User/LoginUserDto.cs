using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.User
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
