using Domain.Entities;
using Domain.Interface;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        /*
            Validação da rota que adiciona Usuário para não permitir e-mail duplicado. 
         */
        public async Task<User> PostUserAsync(User user)
        {
            var emailDb = await GetByEmailAsync(user.Email);
            if (emailDb == null) 
            {
                user.AccessionDate = DateTime.Today;
                await _userRepository.PostUserAsync(user);
                return user;
            }
            else
            {
                Console.WriteLine("E-mail já cadastrado");
                return null;
            }            
            
        }
    }
}