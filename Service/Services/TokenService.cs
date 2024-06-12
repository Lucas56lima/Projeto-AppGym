using Domain.Entities;
using Domain.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public TokenService (IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string> GenerateToken(Login login)
        {
            var userDatabase = await _userRepository.GetByEmailAsync(login.Email);
            if (login.Email != userDatabase.Email || login.Password != userDatabase.Password)
            {
                return string.Empty;
            }               
            
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"] ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var singingCrendentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new[]
                {
                    new Claim(type: ClaimTypes.Name, value: userDatabase.Email),
                    new Claim(type: ClaimTypes.Role, value: userDatabase.Role),

                },
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: singingCrendentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;            
        }
    }
}
