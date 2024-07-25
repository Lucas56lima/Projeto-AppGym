using Domain.Entities;
using Domain.Interface;
using Stripe;
using Plan = Domain.Entities.Plan;

namespace Service.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }
        public async Task<IEnumerable<Plan>> GetAllPlansAsync()
        {
            return await _planRepository.GetAllPlansAsync();
        }

        public async Task<Plan> GetPlanByIdAsync(int id)
        {
            return await _planRepository.GetPlanByIdAsync(id);
        }

        public async Task<Plan> GetPlanByNameAsync(string name)
        {
            return await _planRepository.GetPlanByNameAsync(name);
        }

        public async Task<Plan?> PostPlanAsync(Plan plan)
        {
            try
            {
                var planDb = await _planRepository.GetPlanByNameAsync(plan.Name);
                if (planDb == null)
                {
                    var productOptions = new ProductCreateOptions()
                    {
                        Name = plan.Name,
                    };
                    var productService = new ProductService();
                    var product = await productService.CreateAsync(productOptions);

                    var priceOptions = new PriceCreateOptions()
                    {
                        UnitAmount = (long)plan.Value * 100, // Garantir que UnitAmount é long
                        Currency = "brl",
                        Recurring = new PriceRecurringOptions()
                        {
                            Interval = "month",
                        },
                        Product = product.Id,
                    };
                    var priceService = new PriceService();
                    var price = await priceService.CreateAsync(priceOptions);
                    return await _planRepository.PostPlanAsync(plan);
                }
                else
                {
                    Console.WriteLine("Plano já cadastrado!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o plano: {ex.Message}");
                return null;
            }
        }

        public Task<Plan> PutPlanByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
