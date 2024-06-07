using Domain.Entities;

namespace Domain.Interface
{
    public interface ICustomWorkoutDetailRepository
    {
        Task<CustomWorkoutDetail> GetCustomWorkoutDetailByNameAsync(string name);
        Task<IEnumerable<CustomWorkoutDetail>> GetAllCustomWorkoutsDeatilsAsync();
        Task<CustomWorkoutDetail> PostCustomWorkoutDetailAsync(CustomWorkoutDetail customWorkoutDetail);
    }
}
