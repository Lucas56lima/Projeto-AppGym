using System.Text;
using System.Text.Json;
using Domain.Entities;
using Domain.Interface;
using Stripe;

namespace Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly IPlanRepository _planRepository;
        private readonly IUserRepository _userRepository;
        private readonly HttpClient _httpClient;
        private readonly string _accessToken;
        public PaymentService (IPaymentRepository repository, IPlanRepository planRepository,
            HttpClient httpClient,string accessToken,IUserRepository userRepository)
        {
            _repository = repository;
            _planRepository = planRepository;
            _httpClient = httpClient;
            _accessToken = accessToken;
            _userRepository = userRepository;
        }
        public async Task<string> PostPaymentAsync(Payment payment, string email, string name, string planName)
        {
            var customerOptions = new CustomerCreateOptions
            {
                Email = email,
                Name = name,
                PaymentMethod = "card"
            };
            
            var planDb = await _planRepository.GetPlanByNameAsync(planName);
            //var autoRecurring = new PreapprovalAutoRecurring()
            //{
            //    Frequency = planDb.Frequency,
            //    FrequencyType = planDb.FrequencyType,
            //    CurrencyId = "BRL",
            //    StartDate = DateTime.Now,
            //    TransactionAmount = planDb.Frequency
            //};

            var subscription = new
            {
                Reason = planDb.Name,
                planDb,
                PlayerEmail = email
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.mercadopago.com/v1/preapproval?access_token={_accessToken}")
            {
                Content = new StringContent(JsonSerializer.Serialize(subscription), Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);
            
            jsonResponse.GetProperty("init_point").GetString();
            return await _repository.PostPaymentAsync(payment);

        }       
    }
}
