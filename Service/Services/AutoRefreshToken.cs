using Domain.Interface;
using Jose;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace Service.Services
{
    public class AutoRefreshToken : IAutoRefreshToken
    {
        private readonly RequestDelegate _next;
        private readonly JwtOptions _jwtOptions;

        public AutoRefreshToken (RequestDelegate next, IOptions<JwtOptions> jwtOptions)
        {
            _next = next;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            var expiredToken = false;
            // Verifica se existe um token JWT na requisição
            var token = context.Request.Headers["Authorizarion"].FirstOrDefault()?.Split(" ");
            if(token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    var principal = tokenHandler.ValidateToken();
                }
        }
    }
}
