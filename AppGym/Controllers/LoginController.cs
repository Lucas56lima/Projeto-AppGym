using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public LoginController (ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost(template: "login", Name = "login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostLoginAsync(Login login)
        {
            var token = await _tokenService.GenerateToken(login);
            if (token == "")
                return Unauthorized();
            return Ok(token);
        }
    }
}
