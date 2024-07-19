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
        public async Task<IEnumerable<User>?> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();            
            List<User> usersDecrypters = new List<User>();
            if (users != null)
            {
                foreach(User user in users )
                {
                    string[] emailParts = user.Email.Split("@");
                    var decryptedUser = new User
                    {
                        Name = await _encryptionService.Decrypt(user.Name),
                        Email = await _encryptionService.Decrypt(emailParts[0]) + "@" + emailParts[1],
                        Password = await _encryptionService.Decrypt(user.Password),
                        Fone = await _encryptionService.Decrypt(user.Fone),
                        Birthday = user.Birthday,
                        SpecialCondition = user.SpecialCondition,
                        Plan = user.Plan,
                        AccessionDate = user.AccessionDate,
                        PaymentId = user.PaymentId,
                        Role = user.Role
                    };
                    usersDecrypters.Add(decryptedUser);               
                }
                return usersDecrypters;
            }
            else
            {
                return null;
            }        
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            string[] emailParts = user.Email.Split("@");
            if (user != null && emailParts.Length == 2)
            {
                User decryptedUser = new User
                {
                    Name = await _encryptionService.Decrypt(user.Name),
                    Email = await _encryptionService.Decrypt(emailParts[0]) + "@" + emailParts[1],
                    Password = await _encryptionService.Decrypt(user.Password),
                    Fone = await _encryptionService.Decrypt(user.Fone),
                    Birthday = user.Birthday,
                    SpecialCondition = user.SpecialCondition,
                    Plan = user.Plan,
                    AccessionDate = user.AccessionDate,
                    PaymentId = user.PaymentId,
                    Role = user.Role
                };
                return decryptedUser;

            }
            else
            {
                return null;
            }

        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            string[] emailParts = user.Email.Split("@");
            if (user != null && emailParts.Length == 2)
            {
                User decryptedUser = new User
                {
                    Name = await _encryptionService.Decrypt(user.Name),
                    Email = await _encryptionService.Decrypt(emailParts[0]) + "@" + emailParts[1],
                    Password = await _encryptionService.Decrypt(user.Password),
                    Fone = await _encryptionService.Decrypt(user.Fone),
                    Birthday = user.Birthday,
                    SpecialCondition = user.SpecialCondition,
                    Plan = user.Plan,
                    AccessionDate = user.AccessionDate,
                    PaymentId = user.PaymentId,
                    Role = user.Role
                };
                return decryptedUser;              
                
            }
            else
            {
                return null;
            }
        }
        /*
            Validação da rota que adiciona Usuário para não permitir e-mail duplicado. 
        */
        public async Task<User?> PostUserAsync(User user)
        {
            var emailDb = await GetUserByEmailAsync(user.Email);
            if (emailDb == null) 
            {
                string[] emailParts = user.Email.Split("@");                
                User encryptedUser = new User
                {
                    Name = await _encryptionService.Encrypt(user.Name),
                    Email = await _encryptionService.Encrypt(emailParts[0]) + "@" + emailParts[1],
                    Password = await _encryptionService.Encrypt(user.Password),
                    Fone = await _encryptionService.Encrypt(user.Fone),
                    Birthday = user.Birthday,
                    SpecialCondition = user.SpecialCondition,
                    Plan = user.Plan,
                    AccessionDate = user.AccessionDate,
                    PaymentId = user.PaymentId,
                    Role = user.Role
                };
                return await _userRepository.PostUserAsync(encryptedUser);
            }                                         
            else
            {
                Console.WriteLine("E-mail já cadastrado");
                return null;
            }                     
        }
        public async Task<User?> PostSpecialUserAdminAsync(User specialUser)
        {
            var specialUserEmailDb = await _userRepository.GetUserByEmailAsync(specialUser.Email);
            if (specialUserEmailDb == null)
            {
                string[] emailParts = specialUser.Email.Split("@");
                User encryptedUser = new User
                {
                    Name = await _encryptionService.Encrypt(specialUser.Name),
                    Email = await _encryptionService.Encrypt(emailParts[0]) + "@" + emailParts[1],
                    Password = await _encryptionService.Encrypt(specialUser.Password),
                    Fone = await _encryptionService.Encrypt(specialUser.Fone),
                    Birthday = specialUser.Birthday,
                    SpecialCondition = null,
                    Plan = null,
                    AccessionDate = specialUser.AccessionDate,
                    PaymentId = 0,
                    Role = "admin"
                };
                return await _userRepository.PostSpecialUserAdminAsync(encryptedUser);
            }
            else
            {
                return null;
            }
        }

        public async Task<User?> PostSpecialUserSuperAsync(User specialUser)
        {

            var specialUserEmailDb = await _userRepository.GetUserByEmailAsync(specialUser.Email);
            if (specialUserEmailDb == null)
            {
                string[] emailParts = specialUser.Email.Split("@");
                User encryptedUser = new User
                {
                    Name = await _encryptionService.Encrypt(specialUser.Name),
                    Email = await _encryptionService.Encrypt(emailParts[0]) + "@" + emailParts[1],
                    Password = await _encryptionService.Encrypt(specialUser.Password),
                    Fone = await _encryptionService.Encrypt(specialUser.Fone),
                    Birthday = specialUser.Birthday,
                    SpecialCondition = null,
                    Plan = null,
                    AccessionDate = specialUser.AccessionDate,
                    PaymentId = 0,
                    Role = "super"
                };
                return await _userRepository.PostSpecialUserSuperAsync(encryptedUser);
            }
            else
            {
                return null;
            }
        }
    }
}