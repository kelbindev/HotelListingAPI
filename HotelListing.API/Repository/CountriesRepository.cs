using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDBContext _context;
        private readonly IMapper _mapper;

        public CountriesRepository(HotelListingDBContext context, IMapper mapper) : base(context,mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<Country> GetCountryDetails(int id)
        {
            return await _context.Countries.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.Id == id);   
        }
    }
}
