using Domain.Entities;

namespace Domain.Interface
{
    public interface IPaymentRepository
    {
        Task<string> PostPaymentAsync(Payment payment);
    }
}
