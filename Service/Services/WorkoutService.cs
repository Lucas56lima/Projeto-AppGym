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

        public async Task<Workout> GetWorkoutByNameAsync(string name)
        {
            return await _workoutRepository.GetWorkoutByNameAsync(name);
        }

        public async Task<Workout?> PostWorkoutAsync(Workout workout)
        {
            var workoutDbByName = await GetWorkoutByNameAsync(workout.WorkoutName);
            if (workoutDbByName == null)
            {
                return  await _workoutRepository.PostWorkoutAsync(workout);
            }
            else
            {
                return null;
            }
        }

        public async Task<Workout> PutWorkoutAsync(int id, Workout newWorkout)
        {
            var workout = await GetWorkoutByIdAsync(id);
            newWorkout.WorkoutId = workout.WorkoutId;
            newWorkout.ImplementationDate = workout.ImplementationDate;
            newWorkout.ExpirationDate = workout.ExpirationDate;            
            return await _workoutRepository.PutWorkoutAsync(id, newWorkout);
        }
    }
}
