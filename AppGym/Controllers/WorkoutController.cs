using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    /// <summary>
    /// Controller responsável por operações relacionadas aos treinos.
    /// </summary>
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
        /// <summary>
        /// Registra um novo treino.
        /// </summary>
        /// <param name="workout">Os dados do treino a serem registrados.</param>
        /// <returns>Um Objeto Workout com o treino recém-registrado.</returns>
        [HttpPost("RegisterWorkout")]
        [Authorize(Roles = "admin,super")]
        public async Task<ActionResult<Workout>> PostWorkoutAsync([FromBody]Workout workout)
        {           
            return Ok(await _service.PostWorkoutAsync(workout));
        }
        /// <summary>
        /// Obtém todos os treinos disponíveis.
        /// </summary>        
        /// <returns>Uma lista de Objetos Workout com todos os treinos disponíveis.</returns>
        [HttpGet("ViewAllWorkouts")]
        [Authorize(Roles = "admin,super")]
        public async Task<IActionResult> GetAllWorkoutsAsync()
        {
            return Ok(await _service.GetAllWorkoutsAsync());
        }
        /// <summary>
        /// Obtém um treino pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do treino.</param>
        /// <returns>Um Objeto Workout com o treino com o ID especificado.</returns>
        [HttpGet("ViewWorkoutById/{id}")]
        [Authorize(Roles = "super")]
        public async Task<IActionResult> GetWorkoutByIdAsync(int id)
        {
            return Ok(await _service.GetWorkoutByIdAsync(id));
        }
        /// <summary>
        /// Atualiza um treino pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do treino.</param>
        /// <param name="newWorkout">Objeto do tipo Workout para atualizar o treino</param>
        /// <returns>Um Objeto Workout com o treino atualizdo.</returns>
        [HttpPut("UpdateWorkoutById{id}")]
        [Authorize(Roles = "admin,super")]
        public async Task<IActionResult> PutWorkoutAsync(int id, Workout newWorkout)
        {
            return Ok(await _service.PutWorkoutAsync(id, newWorkout));
        }
    }
}
