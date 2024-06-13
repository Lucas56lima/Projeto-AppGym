using Domain.Entities;

namespace Domain.Interface
{
    public interface ISpecialUserRepository
    {
        Task<SpecialUser> PostSpecialUserAdminAsync(SpecialUser specialUser);
        Task<SpecialUser> PostSpecialUserSuperAsync(SpecialUser specialUser);
        Task<SpecialUser> GetSpecialUserByEmailAsync(string email);        
    }
}
