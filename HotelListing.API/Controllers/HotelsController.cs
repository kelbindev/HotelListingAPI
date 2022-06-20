using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Contracts;
using AutoMapper;
using HotelListing.API.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Models;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository _hotelsRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelsRepository hotelsRepository, IMapper mapper)
        {
            this._hotelsRepository = hotelsRepository;
            this._mapper = mapper;
        }

        // GET: api/Hotels/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetHotelDTO>>> GetHotels()
        {
            var hotels = await _hotelsRepository.GetAllAsync();
            var hotelsDto = _mapper.Map<List<GetHotelDTO>>(hotels);
            return Ok(hotelsDto);
        }
        // GET: api/Hotels/?StartIndex=0&PageSize=3&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<PagedResult<GetHotelDTO>>> GetPagedHotels([FromQuery] QueryParameters queryParameters)
        {
            var hotels = await _hotelsRepository.GetAllAsync<GetHotelDTO>(queryParameters);

            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDTO>> GetHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelDto = _mapper.Map<GetHotelDTO>(hotel);

            return hotelDto;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDTO hotelDto)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var hotel = await _hotelsRepository.GetAsync(hotelDto.Id);

                if (hotel == null)
                {
                    return NotFound();
                }

                _mapper.Map(hotelDto, hotel);
                await _hotelsRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDTO createHotelDto)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDto);

            await _hotelsRepository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (!await HotelExists(id))
            {
                return NotFound();
            }

            await _hotelsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
