namespace Domain.Entities
{
    public class CustomWorkout
    {
        public int CustomWorkoutId { get; set; }
        public required string CustomWorkoutName{ get; set; }
        public required string Finally { get; set; }
        public required string Description { get; set; }
        public required string TrainningPlace { get; set; }
        public DateTime ImplementationDate { get; set; }
        public DateTime ExpirationDate { get; set; }           
        public bool Active { get; set; } = true;

    }
}