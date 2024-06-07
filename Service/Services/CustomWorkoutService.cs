using Domain.Entities;
using Domain.Interface;
using Infrastructure.Repositories;

namespace Service.Services
{
    public class CustomWorkoutService : ICustomWorkoutService
    {
        private readonly ICustomWorkoutRepository _repository;
        public CustomWorkoutService(ICustomWorkoutRepository repository) 
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomWorkout>> GetAllCustomWorkoutsAsync()
        {
            return await _repository.GetAllCustomWorkoutsAsync();
        }

        public async Task<CustomWorkout> GetCustomWorkoutByIdAsync(int id)
        {
            return await _repository.GetCustomWorkoutByIdAsync(id);
        }

        public async Task<CustomWorkout> PostCustomWorkoutAsync(CustomWorkout customWorkout)
        {
            return await _repository.PostCustomWorkoutAsync(customWorkout);
        }
    }
}
