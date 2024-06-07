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
            var newWorkout = new Workout
            {
                Name = workout.Name,
                MuscleGroup = workout.MuscleGroup,
                Video = workout.Video,
                Description = workout.Description,
                TrainningPlace = workout.TrainningPlace,
                ImplementationDate = workout.ImplementationDate,
                ExpirationDate = workout.ExpirationDate,
                Active = workout.Active
            };
            await _workoutRepository.PostWorkoutAsync(newWorkout);
            return newWorkout;
        }
    }
}
