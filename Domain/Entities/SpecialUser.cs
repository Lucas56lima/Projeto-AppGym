namespace Domain.Entities
{
    public class SpecialUser
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }        
        public string Role { get; set; }
        public bool Active { get; set; } = true;
    }
}
