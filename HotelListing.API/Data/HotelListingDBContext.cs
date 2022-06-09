using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDBContext : DbContext
    {
        public HotelListingDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }   
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name="Indonesia",
                    ShortName="ID"
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

            modelBuilder.Entity<Hotel>().HasData(
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
