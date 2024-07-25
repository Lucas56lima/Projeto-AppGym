using Domain.Entities;

namespace Domain.Viewmodel
{
    public class PaymentViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PlanName { get; set; }
        public Decimal PlanValue { get; set; }
        public string PaymentType { get; set; }
        public CardViewModel Card { get; set; }       

    }
}
