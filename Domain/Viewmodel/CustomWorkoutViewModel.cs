using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Viewmodel
{
    public class CustomWorkoutViewModel
    {
        public string Name { get; set; }
        public required string Finally { get; set; }
        public string Description { get; set; }        
        public required string TrainningPlace { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int AmountWorkouts { get; set; }
    }
}
