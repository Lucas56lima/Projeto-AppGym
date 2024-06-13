
using Domain.Entities;
using Domain.Viewmodel;

namespace Domain.Interface
{
    public interface IUserService
    {   
        Task <User>GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task <User>PostUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        
    }
}