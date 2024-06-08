namespace Domain.Entities
{
    public class CustomWorkoutDetail
    {        
        public int Id { get; set; }
        public int CustomWorkoutId { get; set; }        
        public int WorkoutId { get; set; }
        public int Repetitions { get; set; }
        public int Time {get; set; }
        public int Interval { get; set; }
        public int Sequence { get; set; }
    }
}
