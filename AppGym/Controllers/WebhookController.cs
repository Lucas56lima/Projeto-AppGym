using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace AppGym.Controllers
{
    [Route("webhook")]
    [ApiController]
    public class WebhookController : Controller
    {

        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        const string endpointSecret = "sk_test_51PIZ3R05taSXSw8O1ywANBHfrBDXYU9OxTqQCj465U9mYTkW9InUyVrA9niC1vCzQ9n7XixEPnoE3wH7iUhKrKNx00lL1NqCcn";

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    Console.WriteLine("Pagamento realizado com sucesso!");
                }
               
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
        
    }
}

