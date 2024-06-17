using Domain.Entities;
using Domain.Interface;

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

        public async Task<Plan> PostPlanAsync(Plan plan)
        {
            var planDb = await _planRepository.GetPlanByNameAsync(plan.Name);
            if (planDb == null)
            {               
                return await _planRepository.PostPlanAsync(plan);
            }
            else
            {
                Console.WriteLine("Plano já cadastrado!");
                return null;
            }
        }
    }
}
