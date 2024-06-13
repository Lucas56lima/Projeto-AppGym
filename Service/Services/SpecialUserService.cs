using Domain.Entities;
using Domain.Interface;

namespace Service.Services
{
    public class SpecialUserService : ISpecialUserService
    {
        private readonly ISpecialUserRepository _specialRepository;
        public SpecialUserService(ISpecialUserRepository specialRepository)
        {
            _specialRepository = specialRepository;
        }
        public async Task<SpecialUser> GetSpecialUserByEmailAsync(string email)
        {
            return await _specialRepository.GetSpecialUserByEmailAsync(email);
        }

        public async Task<SpecialUser> PostSpecialUserAdminAsync(SpecialUser specialUser)
        {
            var specialUserEmailDb = await _specialRepository.GetSpecialUserByEmailAsync(specialUser.Email);
            if (specialUserEmailDb != null)
            {
                specialUser.Role = "admin";
                return await _specialRepository.PostSpecialUserAdminAsync(specialUser);
            }
            else
            {
                return null;
            }
        }

        public async Task<SpecialUser> PostSpecialUserSuperAsync(SpecialUser specialUser)
        {

            var specialUserEmailDb = await _specialRepository.GetSpecialUserByEmailAsync(specialUser.Email);
            if (specialUserEmailDb != null)
            {
                specialUser.Role = "super";
                return await _specialRepository.PostSpecialUserAdminAsync(specialUser);
            }
            else
            {
                return null;
            }
        }
    }
}
