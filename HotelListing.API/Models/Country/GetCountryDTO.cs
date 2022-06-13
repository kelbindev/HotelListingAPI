using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Models.Country
{
    public class GetCountryDTO : BaseCountryDTO
    {
        public int Id { get; set; }
    }

    public class GetCountryDetailsDTO : BaseCountryDTO
    {
        public int Id { get; set; }
        public List<GetHotelDTO> Hotels { get; set; }
    }
}
