namespace Domain.Entities
{
    public class LogAcess
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime Date { get; set; }
        public string Operation { get; set; }
        public string Details { get; set; }         
    }
}
