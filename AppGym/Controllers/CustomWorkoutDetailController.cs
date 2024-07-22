using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomWorkoutDetailController : ControllerBase
    {
        private readonly ICustomWorkoutDetailService _service;
        public CustomWorkoutDetailController(ICustomWorkoutDetailService service)
        {
            _service = service;
        }

        [HttpPost("RegisterWorkoutDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles ="admin,super")]
        public async Task<IActionResult> PostCustomWorkoutDetailAsync([FromBody] CustomWorkoutDetail customWorkoutDetail)
        {
            return Ok(await _service.PostCustomWorkoutDetailAsync(customWorkoutDetail));
        }
        [HttpGet("ViewCustomsWorkoutsDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomWorkoutsDetailsAsync()
        {
            return Ok(await _service.GetAllCustomWorkoutsDetailsAsync());
        }
        [HttpGet("ViewCustomWorkoutDetailsByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> GetCustomWorkoutDetailByNameAsync(string name)
        {
            return Ok(await _service.GetCustomWorkoutDetailByNameAsync(name));
        }
    }
}
