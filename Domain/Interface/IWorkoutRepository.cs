using Domain.Entities;

namespace Domain.Interface
{
    public interface IWorkoutRepository
    {
        Task<Workout> GetWorkoutByIdAsync(int id);
        Task<IEnumerable<Workout>> GetAllWorkoutsAsync();
        Task <Workout>PostWorkoutAsync(Workout workout);
    }
}
