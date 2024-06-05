using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id{get;set;}
        public required string Name{get;set;}
        public required string Email {get;set;}
        public required string Password{get;set;}
        public required string Fone {get;set;}
        public DateTime Birthday {get;set;}
        public string? SpecialCondition {get;set;}
        public required string Plan {get;set;}
        public DateTime AccessionDate {get;set;}

        public int PaymentId {get;set;}
        public bool Active {get;set;} = true;
                      
    }
}