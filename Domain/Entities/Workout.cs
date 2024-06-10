namespace Domain.Entities
{
    public class Workout
    {
        public int WorkoutId { get; set; }
        public required string WorkoutName{ get; set; }
        public required string MuscleGroup { get; set; }
        public required string Video { get; set; }
        public required string Description { get; set; }
        public required string TrainningPlace { get; set; }
        public DateTime ImplementationDate { get; set; }
        public DateTime ExpirationDate { get; set; }        
        public bool Active { get; set; } = true;
        
    }
}