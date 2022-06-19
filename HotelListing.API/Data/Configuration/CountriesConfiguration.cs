using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configuration
{
    public class CountriesConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country
                {
                    Id = 1,
                    Name = "Indonesia",
                    ShortName = "ID"
                },
                new Country
                {
                    Id = 2,
                    Name = "Singapore",
                    ShortName = "SG"
                },
                new Country
                {
                    Id = 3,
                    Name = "Malaysia",
                    ShortName = "MY"
                }
                );
        }
    }
}
