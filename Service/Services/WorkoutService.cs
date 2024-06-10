using Domain.Entities;
using Domain.Interface;

namespace Service.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutService(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }
        public async Task<IEnumerable<Workout>> GetAllWorkoutsAsync()
        {
            return await _workoutRepository.GetAllWorkoutsAsync();
        }

        public async Task<Workout> GetWorkoutByIdAsync(int id)
        {
            return await _workoutRepository.GetWorkoutByIdAsync(id);
        }

        public async Task<Workout> PostWorkoutAsync(Workout workout)
        {
            return await _workoutRepository.PostWorkoutAsync(workout);
        }

        //public async Task<Workout> PutWorkoutAsync(int customWorkoutDetailId, int workoutId)
        //{
        //   return await _workoutRepository.PutWorkoutAsync(customWorkoutDetailId, workoutId);
        //}
    }
}
