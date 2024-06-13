using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace AppGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin,super,user")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _service;

        public WorkoutController(IWorkoutService service)
        {
            _service = service;
        }

        [HttpPost("RegisterWorkout")]
        [Authorize(Roles = "admin,super")]
        public async Task<ActionResult<Workout>> PostWorkoutAsync([FromBody]Workout workout)
        {           
            return Ok(await _service.PostWorkoutAsync(workout));
        }
        
        [HttpGet("ViewAllWorkouts")]
        [Authorize(Roles = "admin,super")]
        public async Task<IActionResult> GetAllWorkoutsAsync()
        {
            return Ok(await _service.GetAllWorkoutsAsync());
        }

        [HttpGet("ViewWorkoutById/{id}")]
        [Authorize(Roles = "super")]
        public async Task<IActionResult> GetWorkoutByIdAsync(int id)
        {
            return Ok(await _service.GetWorkoutByIdAsync(id));
        }
        //[HttpPut("UpdateCustomWorkouIdInWorkout")]
        //public async Task<IActionResult> PutWorkoutAsync(int customWorkoutId, int workoutId)
        //{
        //    return Ok(await _service.PutWorkoutAsync(customWorkoutId, workoutId));
        //}

    }
}
