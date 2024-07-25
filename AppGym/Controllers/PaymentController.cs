using Domain.Entities;
using Domain.Interface;
using Domain.Viewmodel;
using Microsoft.AspNetCore.Mvc;

namespace AppGym.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _service;
        public PaymentController(IPaymentService service)
        {
            _service = service;
        }
    
    [HttpPost("Payment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]    
    /// <summary>
    /// Registra um novo usuário Super com autorização somente para outro Super.
    /// </summary>
    /// <param name="user">Os dados do usuário a serem registrados.</param>
    /// <returns>O usuário recém-registrado.</returns>
    public async Task<IActionResult> PostPaymentAsync([FromBody]Payment payment, PaymentViewModel paymentViewModel)
        {
            return Ok(await _service.PostPaymentAsync(payment, paymentViewModel));
        }
    }
}
