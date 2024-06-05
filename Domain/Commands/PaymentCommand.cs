using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public class PaymentCommand
    {
        public int Id { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime Duration { get; set; }
        public int UserId { get; set; }
        public bool Active {get;set;} = true;
    }
}