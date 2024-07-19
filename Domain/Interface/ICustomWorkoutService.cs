using Domain.Entities;

namespace Domain.Interface
{
    public interface ICustomWorkoutService
    {
        Task<CustomWorkout> GetCustomWorkoutByIdAsync(int id);
        Task<CustomWorkout> GetCustomWorkoutByNameAsync(string name);
        Task<IEnumerable<CustomWorkout>> GetAllCustomWorkoutsAsync();
        Task<CustomWorkout> PostCustomWorkoutAsync(CustomWorkout customWorkout);
        Task<CustomWorkout> PutCustomWorkoutAsync(int id, CustomWorkout newCustomWorkout);
        Task<string> PutCustomWorkoutInTableDetailsAsync(int id);
    }
}
