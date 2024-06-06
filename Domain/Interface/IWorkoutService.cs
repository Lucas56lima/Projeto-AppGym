using Domain.Entities;

namespace Domain.Interface
{
    public interface IWorkoutService
    {
        Task<Workout> GetWorkoutByIdAsync(int id);
        Task<IEnumerable<Workout>> GetAllWorkoutsAsync();
        Task<Workout> PostWorkoutAsync(Workout workout);
    }
}
