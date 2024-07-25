using Domain.Entities;
using Domain.Viewmodel;

namespace Domain.Interface
{
    public interface IPaymentService
    {
        Task<string> PostPaymentAsync(Payment payment, PaymentViewModel paymentViewModel);        
        Task<string> MethodPaymentCard(CardViewModel cardViewModel);
        Task<string> MethodPaymentIntent(PaymentViewModel paymentViewModel);
        Task<string> GetBoletoUrl(string paymentIntentId);
    }
}
