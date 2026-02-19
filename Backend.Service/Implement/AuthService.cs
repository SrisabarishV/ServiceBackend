using Backend.Common.DTO;
using Backend.Repo.Interface;
using Backend.Service.Interface;
using Backend.SQLContext.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly ITokenBlacklistService _blacklist;

        public AuthService(
            IAuthRepository repo,
            IConfiguration config,
            ITokenBlacklistService blacklist)
        {
            _repo = repo;
            _config = config;
            _blacklist = blacklist;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _repo.GetUserByEmailAsync(request.Email);

            if (user == null)
                return null;

            var validPassword = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.Passwordhash);

            if (!validPassword)
                return null;

            var token = await GenerateJwtAsync(user);

            return new LoginResponseDto
            {
                Token = token,
                UserId = user.Userid,
                RoleId = user.Roleid
            };
        }

        public Task<bool> LogoutAsync(string token)
        {
            _blacklist.Revoke(token);
            return Task.FromResult(true);
        }

        // CHANGE THIS: Method is now Async and returns Task<string>
        private async Task<string> GenerateJwtAsync(User user)
        {
            var key = _config["Jwt:Key"];
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            var claims = new[]
            {
                new Claim("UserId", user.Userid.ToString()),
                new Claim("Email", user.Emailid),
                new Claim("RoleId", user.Roleid?.ToString() ?? "")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            
            var expirationSetting = await _repo.GetAppSettingValueAsync("JwtExpirationMinutes");   // fetch expire Timing From DB

            int expirationMinutes = 60;
            if (!string.IsNullOrEmpty(expirationSetting) && int.TryParse(expirationSetting, out int parsedMinutes))
            {
                expirationMinutes = parsedMinutes;
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes), 
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}