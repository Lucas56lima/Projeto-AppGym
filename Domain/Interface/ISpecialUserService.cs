using Domain.Entities;

namespace Domain.Interface
{
    public interface ISpecialUserService
    {
        Task<SpecialUser> PostSpecialUserAdminAsync(SpecialUser specialUser);
        Task<SpecialUser> PostSpecialUserSuperAsync(SpecialUser specialUser);
        Task<SpecialUser> GetSpecialUserByEmailAsync(string email);
    }
}
