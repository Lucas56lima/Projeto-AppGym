using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomWorkoutDetailController : ControllerBase
    {
        private readonly ICustomWorkoutDetailService _service;
        public CustomWorkoutDetailController(ICustomWorkoutDetailService service)
        {
            _service = service;
        }

        [HttpPost("RegisterWorkoutDetails")]
        public async Task<IActionResult> PostCustomWorkoutDetailAsync([FromBody] CustomWorkoutDetail customWorkoutDetail)
        {
            return Ok(await _service.PostCustomWorkoutDetailAsync(customWorkoutDetail));
        }
        [HttpGet("ViewCustomsWorkoutsDetails")]
        public async Task<IActionResult> GetAllCustomWorkoutsDetailsAsync()
        {
            return Ok(await _service.GetAllCustomWorkoutsDetailsAsync());
        }
        [HttpGet("ViewCustomWorkoutDetailsByName")]
        public async Task<IActionResult> GetCustomWorkoutDetailByNameAsync(string name)
        {
            return Ok(await _service.GetCustomWorkoutDetailByNameAsync(name));
        }
    }
}
