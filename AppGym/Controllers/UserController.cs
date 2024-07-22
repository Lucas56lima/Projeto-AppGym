using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    /// <summary>
    /// Controller responsável por operações relacionadas aos usuários.
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
        /// <returns>O um Objeto User com o usuário recém-registrado.</returns>
        [HttpPost("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostUserAsync([FromBody] User user)
        {
            try
            {                
                return Ok(await _service.PostUserAsync(user));
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
        [HttpPost("ViewUserById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "super,admin")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            return Ok(await _service.GetUserByIdAsync(id));              
        }
        /// <summary>
        /// Registra um novo usuário Admin com autorização de um usuário Super ou Admin.
        /// </summary>
        /// <param name="specialUser">Os dados do usuário a serem registrados.</param>
        /// <returns>O usuário recém-registrado.</returns>
        [HttpPost("RegisterSpecialUserAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "super,admin")]
        public async Task<IActionResult> PostSpecialUserAdminAsync(User specialUser)
        {
            try
            {
                return Ok(await _service.PostSpecialUserAdminAsync(specialUser));
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao registrar o usuário: {ex.Message}");
            }
           
        }
        [HttpPost("RegisterSpecialUserSuper")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "super")]
        /// <summary>
        /// Registra um novo usuário Super com autorização somente para outro Super.
        /// </summary>
        /// <param name="user">Os dados do usuário a serem registrados.</param>
        /// <returns>O usuário recém-registrado.</returns>
        public async Task<IActionResult> PostSpecialUserSuperAsync(User specialUser)
        {
            try
            {
                return Ok(await _service.PostSpecialUserSuperAsync(specialUser));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao registrar o usuário: {ex.Message}");
            }
        }
    }
}
