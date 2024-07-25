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
        private readonly IUserService _userService;
       
        private Timer _timer;
        public TokenService (IConfiguration configuration, IUserService userService)
           
        {
            _configuration = configuration;
            _userService = userService;            
        }        

        public async Task<string> GenerateToken(Login login)
        {
            var userDatabase = await _userService.GetUserByEmailAsync(login.Email);
            Console.WriteLine(userDatabase.Name);
            // Check if valid user credentials
            if (userDatabase == null || login.Email != userDatabase.Email && login.Password != userDatabase.Password)
            {
                return string.Empty;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"] ?? string.Empty));
            string issuer = string.Empty;
            var audience = _configuration["Jwt:Audience"];

            var claims = new List<Claim>();

            if (userDatabase != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, userDatabase.Email));
                claims.Add(new Claim(ClaimTypes.Role, userDatabase.Role));
                issuer = userDatabase.Role;
            }

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }        
      
    }
}
