using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public class WorkoutCommand
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string MuscleGroup { get; set; }
        public required string Video { get; set; }
        public required string Description { get; set; }
        public required string TrainningPlace { get; set; }
        public DateTime ImplementationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Active {get;set;} = true;
    }
}