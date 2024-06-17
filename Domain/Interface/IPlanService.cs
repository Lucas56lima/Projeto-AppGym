using Domain.Entities;

namespace Domain.Interface
{
    public interface IPlanService
    {
        Task<Plan> GetPlanByIdAsync(int id);
        Task<IEnumerable<Plan>> GetAllPlansAsync();
        Task<Plan> PostPlanAsync(Plan plan);
        Task<Plan> GetPlanByNameAsync(string name);
        Task<Plan> PutPlanByNameAsync(string name);
    }
}
