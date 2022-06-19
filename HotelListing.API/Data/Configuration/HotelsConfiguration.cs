using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configuration
{
    public class HotelsConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
               new Hotel
               {
                   Id = 1,
                   Name = "Hotel 1",
                   Address = "Batam, Hotel 1",
                   Rating = 4.5,
                   CountryId = 1
               },
                 new Hotel
                 {
                     Id = 2,
                     Name = "Hotel 2",
                     Address = "Batam, Hotel 2",
                     Rating = 4.5,
                     CountryId = 1
                 },
                  new Hotel
                  {
                      Id = 3,
                      Name = "Hotel 3",
                      Address = "Batam, Hotel 3",
                      Rating = 4.8,
                      CountryId = 2
                  },
                 new Hotel
                 {
                     Id = 4,
                     Name = "Hotel 4",
                     Address = "Batam, Hotel 4",
                     Rating = 4.2,
                     CountryId = 2
                 },
                 new Hotel
                 {
                     Id = 5,
                     Name = "Hotel 5",
                     Address = "Batam, Hotel 5",
                     Rating = 4.8,
                     CountryId = 3
                 },
                 new Hotel
                 {
                     Id = 6,
                     Name = "Hotel 6",
                     Address = "Batam, Hotel 6",
                     Rating = 4.2,
                     CountryId = 3
                 }
                );
        }
    }
}
