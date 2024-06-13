using Domain.Entities;

namespace Domain.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateToken(Login login);
        void GenerateTokenCallback (Login login);
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
        Task Dispose();
    }
}
