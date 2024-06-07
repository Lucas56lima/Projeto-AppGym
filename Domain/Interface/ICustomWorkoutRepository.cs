using Domain.Entities;

namespace Domain.Interface
{
    public interface ICustomWorkoutRepository
    {
        Task<CustomWorkout> GetCustomWorkoutByIdAsync(int id);
        Task<IEnumerable<CustomWorkout>> GetAllCustomWorkoutsAsync();
        Task<CustomWorkout> PostCustomWorkoutAsync(CustomWorkout customWorkout);
    }
}
