using Domain.Entities;

namespace Domain.Interface
{
    public interface ICustomWorkoutService
    {
        Task<CustomWorkout> GetCustomWorkoutByIdAsync(int id);
        Task<IEnumerable<CustomWorkout>> GetAllCustomWorkoutsAsync();
        Task<CustomWorkout> PostCustomWorkoutAsync(CustomWorkout customWorkout);
    }
}
