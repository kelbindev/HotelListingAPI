using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Country
            CreateMap<Country, PutCountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Country, GetCountryDTO>().ReverseMap();
            CreateMap<Country, GetCountryDetailsDTO>().ReverseMap();

            //Hotel
            CreateMap<Hotel, GetHotelDTO>().ReverseMap();
        }
    }
}
