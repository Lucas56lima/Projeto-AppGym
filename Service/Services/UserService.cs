using System;
using System.Diagnostics.Tracing;
using System.Net.Mail;
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
                foreach (User user in users)
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
            // Verifica se o e-mail é válido e não está vazio
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("O e-mail fornecido é inválido.", nameof(email));
            }
            string[] emailParts = email.Split("@");
            var encryptedEmail = await _encryptionService.Encrypt(emailParts[0]) + "@" + emailParts[1];
            var user = await _userRepository.GetUserByEmailAsync(encryptedEmail);
            
            // Verifica se o usuário foi encontrado
            if (user == null)
            {
                return null;
            }
            
            if (emailParts.Length != 2)
            {
                // Adicione um log ou mensagem de erro se necessário
                throw new InvalidOperationException("O e-mail do usuário não tem o formato esperado.");
            }
            string[] userEmailPars = user.Email.Split("@");
            // Cria o usuário descriptografado
            User decryptedUser = new User
            {
                Name = await _encryptionService.Decrypt(user.Name),
                Email = await _encryptionService.Decrypt(userEmailPars[0]) + "@" + userEmailPars[1],
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
            var emailDb = await _userRepository.GetUserByEmailAsync(user.Email.ToLower());
            var validationEmail = IsValidateEmail(user.Email.ToLower());
            if (!validationEmail)
            {
                Console.WriteLine("E-mail inválido.");
                return null;
            }
            if (emailDb != null)
            {
                Console.WriteLine("E-mail já cadastrado");
                return null;
            }
            string[] emailParts = user.Email.Split("@");
            User encryptedUser = new User
            {
                Name = await _encryptionService.Encrypt(user.Name.ToLower()),
                Email = await _encryptionService.Encrypt(emailParts[0].ToLower()) + "@" + emailParts[1].ToLower(),
                Password = await _encryptionService.Encrypt(user.Password),
                Fone = await _encryptionService.Encrypt(user.Fone),
                Birthday = user.Birthday,
                SpecialCondition = user.SpecialCondition.ToLower(),
                Plan = user.Plan.ToLower(),
                AccessionDate = user.AccessionDate,
                PaymentId = user.PaymentId,
                Role = "user"
            };

            return await _userRepository.PostUserAsync(encryptedUser);
        }
        public async Task<User?> PostSpecialUserAdminAsync(User specialUser)
        {
            var specialUserEmailDb = await _userRepository.GetUserByEmailAsync(specialUser.Email.ToLower());
            var validationEmail = IsValidateEmail(specialUser.Email.ToLower());
            if (!validationEmail)
            {
                Console.WriteLine("E-mail inválido.");
                return null;
            }
            if (specialUserEmailDb != null)
            {
                Console.WriteLine("E-mail já cadastrado");
                return null;
            }

            string[] emailParts = specialUser.Email.Split("@");
            User encryptedUser = new User
            {
                Name = await _encryptionService.Encrypt(specialUser.Name.ToLower()),
                Email = await _encryptionService.Encrypt(emailParts[0].ToLower()) + "@" + emailParts[1].ToLower(),
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

        public async Task<User?> PostSpecialUserSuperAsync(User specialUser)
        {

            var specialUserEmailDb = await _userRepository.GetUserByEmailAsync(specialUser.Email.ToLower());
            var validationEmail = IsValidateEmail(specialUser.Email.ToLower());
            if (!validationEmail)
            {
                Console.WriteLine("E-mail inválido.");
                return null;
            }
            if (specialUserEmailDb != null)
            {
                Console.WriteLine("E-mail já cadastrado");
                return null;
            }

            string[] emailParts = specialUser.Email.Split("@");
            User encryptedUser = new User
            {
                Name = await _encryptionService.Encrypt(specialUser.Name.ToLower()),
                Email = await _encryptionService.Encrypt(emailParts[0].ToLower()) + "@" + emailParts[1].ToLower(),
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

        public bool IsValidateEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}