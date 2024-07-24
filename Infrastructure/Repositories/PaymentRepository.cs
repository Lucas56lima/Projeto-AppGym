using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Infrastructure.Handler;
using Microsoft.Data.Sqlite;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppGymContextDb _context;

        public PaymentRepository(AppGymContextDb context)
        {
            _context = context;
        }

        public async Task<string> PostPaymentAsync(Payment payment)
        {
            try
            {
                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
                return "Pagamento salvo!";
            }
            catch (SqliteException ex)
            {
                ErrorHandler.HandlerSqliteException(ex);
                return null;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex);
                return null;
            }
        }
    }
}
