using Domain.Entities;

namespace Domain.Interface
{
    public interface IWorkoutRepository
    {
        Task<Workout> GetWorkoutByIdAsync(int id);
        Task<Workout> GetWorkoutByNameAsync(string name);
        Task<IEnumerable<Workout>> GetAllWorkoutsAsync();
        Task <Workout>PostWorkoutAsync(Workout workout);
        Task<Workout> PutWorkoutAsync(int id, Workout newWorkout);
    }
}
