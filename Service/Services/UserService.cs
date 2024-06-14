using System;
using System.Text;
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
            var users = await _userRepository.GetAllUsersAsync();
            if (users != null)
            {
                //var decryptedUser = new User
                //{
                //    Name = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Name)),
                //    Email = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Email)),
                //    Password = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Password)),
                //    Fone = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Fone)),
                //    Birthday = encryptedUser.Birthday,
                //    SpecialCondition = encryptedUser.SpecialCondition,
                //    Plan = encryptedUser.Plan,
                //    AccessionDate = encryptedUser.AccessionDate,
                //    PaymentId = encryptedUser.PaymentId,
                //    Role = encryptedUser.Role,
                return users;

            }
            else
            {
                return null;
            }        
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user != null)
            {
                //var decryptedUser = new User
                //{
                //    Name = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Name)),
                //    Email = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Email)),
                //    Password = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Password)),
                //    Fone = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Fone)),
                //    Birthday = encryptedUser.Birthday,
                //    SpecialCondition = encryptedUser.SpecialCondition,
                //    Plan = encryptedUser.Plan,
                //    AccessionDate = encryptedUser.AccessionDate,
                //    PaymentId = encryptedUser.PaymentId,
                //    Role = encryptedUser.Role,
                return user;

            }
            else
            {
                return null;
            }

        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                //var decryptedUser = new User
                //{
                //    Name = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Name)),
                //    Email = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Email)),
                //    Password = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Password)),
                //    Fone = await _encryptionService.Decrypt(Encoding.UTF8.GetBytes(encryptedUser.Fone)),
                //    Birthday = encryptedUser.Birthday,
                //    SpecialCondition = encryptedUser.SpecialCondition,
                //    Plan = encryptedUser.Plan,
                //    AccessionDate = encryptedUser.AccessionDate,
                //    PaymentId = encryptedUser.PaymentId,
                //    Role = encryptedUser.Role,
                return user;              
                
            }
            else
            {
                return null;
            }
        }
        /*
            Validação da rota que adiciona Usuário para não permitir e-mail duplicado. 
        */
        public async Task<User> PostUserAsync(User user)
        {
            var emailDb = await GetUserByEmailAsync(user.Email);
            if (emailDb == null) 
            {
                //string encryptedName = await _encryptionService.Encrypt(BitConverter.ToString(Encoding.UTF8.GetBytes(user.Name)))
                //var decryptedUser = new User
                //{
                //    Name = await _encryptionService.Encrypt(BitConverter.ToString(Encoding.UTF8.GetBytes(user.Name))),
                //    Email = await _encryptionService.Encrypt(Encoding.UTF8.GetBytes(user.Email)),
                //    Password = await _encryptionService.Encrypt(Encoding.UTF8.GetBytes(user.Password)),
                //    Fone = await _encryptionService.Encrypt(Encoding.UTF8.GetBytes(user.Fone)),
                //    Birthday = user.Birthday,
                //    SpecialCondition = user.SpecialCondition,
                //    Plan = user.Plan,
                //    AccessionDate = user.AccessionDate,
                //    PaymentId = user.PaymentId,
                //    Role = user.Role,
                return await _userRepository.PostUserAsync(user);
            }                                         
            else
            {
                Console.WriteLine("E-mail já cadastrado");
                return null;
            }           
            
        }
        public async Task<User> PostSpecialUserAdminAsync(User specialUser)
        {
            var specialUserEmailDb = await _userRepository.GetUserByEmailAsync(specialUser.Email);
            if (specialUserEmailDb == null)
            {
                specialUser.Role = "admin";
                return await _userRepository.PostSpecialUserAdminAsync(specialUser);
            }
            else
            {
                return null;
            }
        }

        public async Task<User> PostSpecialUserSuperAsync(User specialUser)
        {

            var specialUserEmailDb = await _userRepository.GetUserByEmailAsync(specialUser.Email);
            if (specialUserEmailDb == null)
            {
                specialUser.Role = "super";
                return await _userRepository.PostSpecialUserSuperAsync(specialUser);
            }
            else
            {
                return null;
            }
        }
    }
}