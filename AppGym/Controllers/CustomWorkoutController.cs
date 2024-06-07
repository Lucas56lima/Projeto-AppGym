using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomWorkoutController : ControllerBase
    {
        private readonly ICustomWorkoutService _service;
        public CustomWorkoutController(ICustomWorkoutService service)
        {
            _service = service;
        }

        [HttpPost("RegisterCustomWorkout")]
        public async Task<IActionResult> PostCustomWorkoutAsync([FromBody] CustomWorkout customWorkout)
        {
            return Ok(await _service.PostCustomWorkoutAsync(customWorkout));
        }
        [HttpGet("ViewAllCustomWorkouts")]
        public async Task<IActionResult> GetAllCustomWorkoutsAsync()
        {
            return Ok(await _service.GetAllCustomWorkoutsAsync());
        }
        [HttpGet("ViewCustomWorkoutsById")]
        public async Task<IActionResult> GetCustomWorkoutByIdAsync(int id)
        {
            return Ok(await _service.GetCustomWorkoutByIdAsync(id));
        }
    }
}
