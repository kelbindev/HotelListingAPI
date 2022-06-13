using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Country
{
    public class PutCountryDTO : BaseCountryDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
