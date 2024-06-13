using Domain.Entities;

namespace Domain.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateToken(Login login);             
    }
}
