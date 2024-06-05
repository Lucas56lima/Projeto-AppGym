using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppGymContextDb _context;

        public UserRepository(AppGymContextDb context){
            _context = context;
        }

       public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task PostUserAsync(User user)
        {
             await _context.Users.AddAsync(user);
             await _context.SaveChangesAsync();
        }
    }
}