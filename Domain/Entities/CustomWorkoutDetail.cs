namespace Domain.Entities
{
    public class CustomWorkoutDetail
    {        
        public int? CustomWorkoutId { get; set; }
        public CustomWorkout CustomWorkout { get; set; }
        public int? WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public int Sequence { get; set; }
    }
}
