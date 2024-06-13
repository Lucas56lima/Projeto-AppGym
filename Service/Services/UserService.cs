using System;
using Domain.Entities;
using Domain.Interface;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        public UserService(IUserRepository userRepository, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
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
            var emailDb = await GetUserByEmailAsync(user.Email);
            if (emailDb == null) 
            {                
                string encryptedEmail = await _encryptionService.Encrypt(user.Email);
                string encryptedPassword = await _encryptionService.Encrypt(user.Password);
                string encryptedFone = await _encryptionService.Encrypt(user.Fone);

                user.AccessionDate = DateTime.Today;
                user.Role = "user";
                user.Email = encryptedEmail;
                user.Password = encryptedPassword;
                user.Fone = encryptedFone;
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