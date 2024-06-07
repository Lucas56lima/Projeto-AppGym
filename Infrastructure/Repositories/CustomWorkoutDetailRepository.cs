using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomWorkoutDetailRepository : ICustomWorkoutDetailRepository
    {
        private readonly AppGymContextDb _context;
        public CustomWorkoutDetailRepository(AppGymContextDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomWorkoutDetail>> GetAllCustomWorkoutsDeatilsAsync()
        {
            return await _context.CustomWorkoutDetails.ToListAsync();
        }

        public async Task<CustomWorkoutDetail> GetCustomWorkoutDetailByNameAsync(string name)
        {
            return await _context.CustomWorkoutDetails.FindAsync(name);
        }

        public async Task<CustomWorkoutDetail> PostCustomWorkoutDetailAsync(CustomWorkoutDetail customWorkoutDetail)
        {
            await _context.AddAsync(customWorkoutDetail);
            _context.SaveChanges();
            return customWorkoutDetail;
        }
    }
}
