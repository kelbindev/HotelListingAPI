using HotelListing.API.Contracts;
using HotelListing.API.Exceptions;
using HotelListing.API.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<UserController> _logger;

        public UserController(IAuthManager authManager, ILogger<UserController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
        }

        //POST: api/User/login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            throw new NotFoundException(nameof(Login),loginUserDto.UserName);

            var authResponse = await _authManager.Login(loginUserDto);

            if (authResponse == null) return Unauthorized();

            return Ok(authResponse);
        }

        //POST: api/User/register
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            Convert.ToInt32("abc");
            var errors = await _authManager.Register(registerUserDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        //POST: api/User/login
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null) return Unauthorized();

            return Ok(authResponse);
        }
    }
}
