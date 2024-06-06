using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpPost("RegisterUser")]       
        public async Task<IActionResult> PostUserAsync([FromBody] User user)
        {
            var newUser = await _service.PostUserAsync(user);
            return Ok(newUser);

        }


        [HttpGet("ViewUserById/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {            
            return Ok(await _service.GetUserByIdAsync(id));
        }
    }
}
