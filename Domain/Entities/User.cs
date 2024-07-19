namespace Domain.Entities
{
    public class User
    {
        public int Id{get;set;}
        public  string Name{get;set;}        
        public  string Email {get;set;}
        public  string Password{get;set;}        
        public  string Fone {get;set;}
        public DateTime Birthday {get;set;}
        public string SpecialCondition {get;set;}
        public  string Plan {get;set;}
        public DateTime AccessionDate {get;set;}
        public int PaymentId {get;set;}
        public string Role {get;set;}
        public bool Active { get; set; } = true;                              
    }
}