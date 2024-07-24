using Domain.Entities;

namespace Domain.Interface
{
    public interface IPaymentService
    {
        Task<string> PostPaymentAsync(Payment payment, string email, string name, string planName);
    }
}
