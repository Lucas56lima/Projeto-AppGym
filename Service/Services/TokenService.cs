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
        private readonly ISpecialUserRepository _specialUserRepository;
        private Timer _timer;
        public TokenService (IConfiguration configuration, IUserRepository userRepository,
            ISpecialUserRepository specialUserRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _specialUserRepository = specialUserRepository;
        }        

        public async Task<string> GenerateToken(Login login)
        {
            var userDatabase = await _userRepository.GetUserByEmailAsync(login.Email);
            var specialUserDatabase = await _specialUserRepository.GetSpecialUserByEmailAsync(login.Email);

            // Check if valid user credentials
            if (userDatabase == null || login.Password != userDatabase.Password)
            {
                // Check if valid special user credentials
                if (specialUserDatabase == null || login.Password != specialUserDatabase.Password)
                {
                    return string.Empty;
                }
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

            if (specialUserDatabase != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, specialUserDatabase.Email));
                claims.Add(new Claim(ClaimTypes.Role, specialUserDatabase.Role));
                issuer = specialUserDatabase.Role;
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
