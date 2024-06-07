using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomWorkoutRepository : ICustomWorkoutRepository
    {
        private readonly AppGymContextDb _context;
        public CustomWorkoutRepository(AppGymContextDb context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CustomWorkout>> GetAllCustomWorkoutsAsync()
        {
            return await _context.CustomWorkouts.ToListAsync();
        }

        public async Task<CustomWorkout> GetCustomWorkoutByIdAsync(int id)
        {
            return await _context.CustomWorkouts.FindAsync(id);
        }

        public async Task<CustomWorkout> PostCustomWorkoutAsync(CustomWorkout customWorkout)
        {
            await _context.CustomWorkouts.AddAsync(customWorkout);
            _context.SaveChanges();
            return customWorkout;
        }
    }
}
