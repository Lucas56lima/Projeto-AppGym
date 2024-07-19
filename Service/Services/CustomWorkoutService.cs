using Domain.Entities;
using Domain.Interface;

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

        public async Task<CustomWorkout> GetCustomWorkoutByNameAsync(string name)
        {
            return await _repository.GetCustomWorkoutByNameAsync(name);
        }

        public async Task<CustomWorkout?> PostCustomWorkoutAsync(CustomWorkout customWorkout)
        {
            var customWorkoutDb = await _repository.GetCustomWorkoutByNameAsync(customWorkout.CustomWorkoutName);
            if (customWorkoutDb != null)
            {
                return null;
            }

            return await _repository.PostCustomWorkoutAsync(customWorkout);
        }

        public async Task<CustomWorkout> PutCustomWorkoutAsync(int id, CustomWorkout newCustomWorkout)
        {
            var customWorkout = await GetCustomWorkoutByIdAsync(id);
            newCustomWorkout.CustomWorkoutId = customWorkout.CustomWorkoutId;
            newCustomWorkout.ImplementationDate = customWorkout.ImplementationDate;
            newCustomWorkout.ExpirationDate = customWorkout.ExpirationDate;            
            return await _repository.PutCustomWorkoutAsync(id, newCustomWorkout); 
        }

        public Task<string> PutCustomWorkoutInTableDetailsAsync(int id)
        {
            return _repository.PutCustomWorkoutInTableDetailsAsync(id);
        }
    }
}
