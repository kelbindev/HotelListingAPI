using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;

        private const string _tokenProvider = "HotelListingAPI";
        private const string _purpose = "RefreshToken";

        public AuthManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _tokenProvider, _purpose);
            var refreshToken = await _userManager.GenerateUserTokenAsync(_user, _tokenProvider, _purpose);
            var result = await _userManager.SetAuthenticationTokenAsync(_user, _tokenProvider, _purpose, refreshToken);
            return refreshToken;
        }

        public async Task<AuthResponseDto> Login(LoginUserDto loginUserDto)
        {
            bool isValidLogin = false;

            _user = await _userManager.FindByNameAsync(loginUserDto.UserName);
            isValidLogin = await _userManager.CheckPasswordAsync(_user, loginUserDto.Password);


            if (!isValidLogin || _user == null)
            {
                return null;
            }
            var token = await GenerateToken();

            return new AuthResponseDto
            {
                UserId = _user.Id,
                Token = token,
                RefreshToken = await CreateRefreshToken()
            };
        }

        public async Task<IEnumerable<IdentityError>> Register(RegisterUserDto registerUserDto)
        {
            _user = _mapper.Map<User>(registerUserDto);
            _user.UserName = registerUserDto.Email;

            var result = await _userManager.CreateAsync(_user, registerUserDto.Password);

            if (result.Succeeded)
            {
                if (registerUserDto.isAdmin)
                {
                    await _userManager.AddToRoleAsync(_user, "Administrator");
                }
                else
                {
                    await _userManager.AddToRoleAsync(_user, "User");
                }
            }

            return result.Errors;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Sub)?.Value;
            _user = await _userManager.FindByNameAsync(username);

            if (_user == null || _user.Id != request.UserId) return null;

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user,_tokenProvider,_purpose, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }

        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("Uid", _user.Id),
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
   
        
    }
}
