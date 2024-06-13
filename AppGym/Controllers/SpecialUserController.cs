using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "super,admin")]
    public class SpecialUserController : ControllerBase
    {
        private readonly ISpecialUserService _specialService;
        public SpecialUserController(ISpecialUserService specialService)
        {
            _specialService = specialService;
        }

        [HttpPost("RegisterSpecialUserAdmin")]        
        public async Task<IActionResult> PostSpecialUserAdminAsync(SpecialUser specialUser)
        {
            return Ok(await _specialService.PostSpecialUserAdminAsync(specialUser));
        }
        [HttpPost("RegisterSpecialUserSuper")]
        [Authorize(Roles = "super")]
        public async Task<IActionResult> PostSpecialUserSuperAsync(SpecialUser specialUser)
        {
            return Ok(await _specialService.PostSpecialUserSuperAsync(specialUser));
        }
    }
}
