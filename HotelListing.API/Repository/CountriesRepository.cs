using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDBContext _context;

        public CountriesRepository(HotelListingDBContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Country> GetCountryDetails(int id)
        {
            return await _context.Countries.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.Id == id);   
        }
    }
}
