using Backend.Common;
using Backend.Common.DTO;
using Backend.Repo.Interface;
using Backend.Service.Interface;
using Backend.SQLContext.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

            var token = GenerateJwt(user);

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

        private string GenerateJwt(User user)
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

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!));

            var creds = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
