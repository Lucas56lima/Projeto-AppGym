using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    /// <summary>
    /// Controller responsável por operações relacionadas a usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="user">Os dados do usuário a serem registrados.</param>
        /// <returns>O usuário recém-registrado.</returns>
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> PostUserAsync([FromBody] User user)
        {
            try
            {
                var newUser = await _service.PostUserAsync(user);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao registrar o usuário: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do usuário.</param>
        /// <returns>O usuário com o ID especificado.</returns>
        [Authorize(Roles = "admin")]
        [HttpGet("ViewUserById/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _service.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"Usuário com ID {id} não encontrado");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter o usuário: {ex.Message}");
            }
        }
    }
}
