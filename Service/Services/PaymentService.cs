using Domain.Entities;
using Domain.Interface;
using Domain.Viewmodel;
using Stripe;
using Stripe.FinancialConnections;

namespace Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly IPlanRepository _planRepository;
        private readonly IUserRepository _userRepository;        
        public PaymentService (IPaymentRepository repository, IPlanRepository planRepository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _planRepository = planRepository;            
            _userRepository = userRepository;
        }

        public async Task<string> PostPaymentAsync(Payment payment, PaymentViewModel paymentViewModel)
        {
            if (paymentViewModel.PaymentType == "card")
            {
                var createMethodPayment = await MethodPaymentCard(paymentViewModel.Card);
                var customerOptions = new CustomerCreateOptions
                {
                    Email = paymentViewModel.Email,
                    Name = paymentViewModel.Name,
                    PaymentMethod = createMethodPayment
                };
                var customerService = new CustomerService();
                var customer = await customerService.CreateAsync(customerOptions);
                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    Amount = (long)(paymentViewModel.PlanValue * 100), // Valor em centavos
                    Currency = "brl",
                    PaymentMethod = "card", // ID do método de pagamento
                    ConfirmationMethod = "automatic",
                    Confirm = true, // Confirma automaticamente
                };
            }
            else
            {
                var createMethodPayment = await MethodPaymentIntent(paymentViewModel);

                var boletoUrl = GetBoletoUrl(createMethodPayment);
                Console.WriteLine(boletoUrl);
            }

            var planDb = await _planRepository.GetPlanByNameAsync(paymentViewModel.PlanName);
           
            return await _repository.PostPaymentAsync(payment);

        }

        public async Task<string> MethodPaymentCard(CardViewModel cardViewModel)
        {
            var paymentMethodOptions = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Number = cardViewModel.Number,
                    ExpMonth = cardViewModel.ExpMonth,
                    ExpYear = cardViewModel.ExpYear,
                    Cvc = cardViewModel.Cvc
                },                
                
            };

            var paymentMethodService = new PaymentMethodService();
            var paymentMethod = await paymentMethodService.CreateAsync(paymentMethodOptions);
            string paymentMethodId = paymentMethod.Id;
            return paymentMethodId;
        }

        public async Task<string> MethodPaymentIntent(PaymentViewModel paymentViewModel)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(paymentViewModel.PlanValue * 100), // Valor em centavos
                Currency = "brl",
                PaymentMethodTypes = new List<string> { "boleto" }, // Método de pagamento boleto
                ConfirmationMethod = "automatic",
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return paymentIntent.Id;
        }

        public async Task<string> GetBoletoUrl(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            var paymentIntent = await service.GetAsync(paymentIntentId);

            var paymentMethod = paymentIntent.PaymentMethod;
            var chargeService = new ChargeService();            
            var charges = await chargeService.ListAsync(new ChargeListOptions
            {
                PaymentIntent = paymentIntentId
            });

            // Encontrar a primeira cobrança
            var charge = charges.Data.FirstOrDefault();
            if (charge != null && charge.PaymentMethodDetails?.Type == "boleto")
            {
                // A URL do boleto está disponível na resposta do charge
                return charge.ToJson();
            }

            return string.Empty;
        }
    }
}
