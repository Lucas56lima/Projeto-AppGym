using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IUserRepository   
    {
         Task <User>GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task PostUserAsync(User user); 
    }
}