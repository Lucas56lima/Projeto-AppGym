using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _service;

        public WorkoutController(IWorkoutService service)
        {
            _service = service;
        }

        [HttpPost("RegisterWorkout")]        
        public async Task<ActionResult<Workout>> PostWorkoutAsync([FromBody]Workout workout)
        {
            var newWorkout = await _service.PostWorkoutAsync(workout);
            return Ok(newWorkout);
        }
        [HttpGet("ViewAllWorkouts")]
        public async Task<IActionResult> GetAllWorkoutsAsync()
        {
            return Ok(await _service.GetAllWorkoutsAsync());
        }

        [HttpGet("ViewWorkoutById/{id}")]        
        public async Task<IActionResult> GetWorkoutByIdAsync(int id)
        {
            return Ok(await _service.GetWorkoutByIdAsync(id));
        }
    }
}
