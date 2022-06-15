using HotelListing.API.Contracts;
using HotelListing.API.Data;

namespace HotelListing.API.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        private readonly HotelListingDBContext _context;

        public HotelsRepository(HotelListingDBContext context) : base(context)
        {
            this._context = context;
        }
    }
}
