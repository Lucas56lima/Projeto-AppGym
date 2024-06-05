using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime Duration { get; set; }
        public int UserId { get; set; }
        public bool Active {get;set;} = true;
    }
}