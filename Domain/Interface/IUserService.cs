
using Domain.Entities;

namespace Domain.Interface
{
    public interface IUserService
    {   
        Task <User>GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task PostUserAsync(User user);        
    }
}