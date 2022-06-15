using HotelListing.API.Configuration;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add connection string
var ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<HotelListingDBContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

//Configure Serilog
builder.Host.UseSerilog((ctx,lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

//Configure Auto mapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

//Configure Repository
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IHotelsRepository, HotelsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
