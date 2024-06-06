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
        public async Task<IActionResult> PostWorkoutAsync([FromBody]Workout workout)
        {
            var newWorkout = await _service.PostWorkoutAsync(workout);
            return Ok(newWorkout);
        }

        [HttpGet("ViewWorkoutById/{id}")]        
        public async Task<IActionResult> GetWorkouByIdAsync(int id)
        {
            return Ok(await _service.GetWorkoutByIdAsync(id));
        }
    }
}
