namespace HotelListing.API.Models
{
    public class QueryParameters
    {
        private int _pageSize = 2;
        public int StartIndex { get; set; }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

    }
}
