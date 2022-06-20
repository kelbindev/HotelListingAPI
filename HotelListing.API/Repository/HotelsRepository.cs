using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;

namespace HotelListing.API.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        private readonly HotelListingDBContext _context;
        private readonly IMapper _mapper;

        public HotelsRepository(HotelListingDBContext context, IMapper mapper) : base(context,mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}
