namespace Domain.Viewmodel
{
    public class UserViewModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Fone { get; set; }
        public DateTime Birthday { get; set; }
        public string SpecialCondition { get; set; }
        public required string Plan { get; set; }
        public DateTime AccessionDate { get; set; }
    }
}
