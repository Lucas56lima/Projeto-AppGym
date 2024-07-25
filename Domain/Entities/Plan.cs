using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Decimal Value { get; set; }
        public required int Duration { get; set; }
        public bool Active { get; set; } = true;        

    }
}