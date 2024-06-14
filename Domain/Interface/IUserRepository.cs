using Domain.Entities;
using Domain.Viewmodel;

namespace Domain.Interface
{
    public interface IUserRepository   
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task <User>PostUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> PostSpecialUserAdminAsync(User specialUser);
        Task<User> PostSpecialUserSuperAsync(User specialUser);       
    }
}