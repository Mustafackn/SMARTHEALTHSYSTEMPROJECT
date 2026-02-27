using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartHealthSystem.Application.Abstractions.Tokenss;
using SmartHealthSystem.Application.DTOs.Tokens;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartHealthSystem.Infrastructure.Services
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(int minute, AppUser user)
        {
            var token = new Token();
            // appsettings.json dosyasındaki gizli anahtarı alıyoruz
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            token.Expiration = DateTime.Now.AddMinutes(minute);

            // Token'ın içine kullanıcının kimlik bilgilerini (Claim) gömüyoruz
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                claims: claims
            );

            token.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}
