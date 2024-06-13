using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin,super,user")]
    public class CustomWorkoutController : ControllerBase
    {
        private readonly ICustomWorkoutService _service;
        public CustomWorkoutController(ICustomWorkoutService service)
        {
            _service = service;
        }

        [HttpPost("RegisterCustomWorkout")]
        [Authorize(Roles = "admin,super")]
        public async Task<IActionResult> PostCustomWorkoutAsync([FromBody] CustomWorkout customWorkout)
        {
            return Ok(await _service.PostCustomWorkoutAsync(customWorkout));
        }
        [HttpGet("ViewAllCustomWorkouts")]
        [Authorize(Roles = "admin,super")]
        public async Task<IActionResult> GetAllCustomWorkoutsAsync()
        {
            return Ok(await _service.GetAllCustomWorkoutsAsync());
        }
        [HttpGet("ViewCustomWorkoutsById")]
        [Authorize(Roles = "super")]
        public async Task<IActionResult> GetCustomWorkoutByIdAsync(int id)
        {
            return Ok(await _service.GetCustomWorkoutByIdAsync(id));
        }
    }
}
