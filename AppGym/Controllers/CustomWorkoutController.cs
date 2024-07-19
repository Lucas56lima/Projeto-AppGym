using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    /// <summary>
    /// Controller responsável por operações relacionadas aos treinos personalizados.
    /// </summary>
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
        /// <summary>
        /// Registra um novo treino personalizado.
        /// </summary>
        /// <param name="customWorkout">Os dados do treino personalizado a serem registrados.</param>
        /// <returns>Um Objeto CustomWorkout com o treino personalizado recém-registrado.</returns>
        [HttpPost("RegisterCustomWorkout")]
        [Authorize(Roles = "admin,super")]
        public async Task<IActionResult> PostCustomWorkoutAsync([FromBody] CustomWorkout customWorkout)
        {
            return Ok(await _service.PostCustomWorkoutAsync(customWorkout));
        }
        /// <summary>
        /// Obtém todos os treinos personalizados disponíveis.
        /// </summary>        
        /// <returns>Uma lista de Objetos CustomWorkout com todos os treinos personalizados disponíveis.</returns>
        [HttpGet("ViewAllCustomWorkouts")]
        [Authorize(Roles = "admin,super")]
        public async Task<IActionResult> GetAllCustomWorkoutsAsync()
        {
            return Ok(await _service.GetAllCustomWorkoutsAsync());
        }
        /// <summary>
        /// Obtém um treino personalizado pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do treino personalizado.</param>
        /// <returns>Um Objeto CustomWorkout com o treino personalizado com o ID especificado.</returns>
        [HttpGet("ViewCustomWorkoutsById")]
        [Authorize(Roles = "super")]
        public async Task<IActionResult> GetCustomWorkoutByIdAsync(int id)
        {
            return Ok(await _service.GetCustomWorkoutByIdAsync(id));
        }
        /// <summary>
        /// Atualiza um treino personalizado pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do treino personalizado.</param>
        /// <param name="newCustomWorkout">Objeto do tipo CustomWorkout para atualizar o treino personalizado</param>
        /// <returns>Um Objeto CustomWorkout com o treino personalizado atualizdo.</returns>
        [HttpPut("UpdateCustomWorkoutById{id}")]
        [Authorize(Roles = "admin,super")]
        public async Task<IActionResult> PutCustomWorkoutAsync(int id, CustomWorkout newCustomWorkout)
        {
            return Ok(await _service.PutCustomWorkoutAsync(id, newCustomWorkout));
        }
    }
}
