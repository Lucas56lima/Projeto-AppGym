using Domain.Entities;
using Domain.Viewmodel;

namespace Domain.Interface
{
    public interface ICustomWorkoutDetailRepository
    {
        Task<CustomWorkoutDetailViewModel> GetCustomWorkoutDetailByNameAsync(string name);
        Task<IEnumerable<CustomWorkoutDetailViewModel>> GetAllCustomWorkoutsDetailsAsync();
        Task<CustomWorkoutDetail> PostCustomWorkoutDetailAsync(CustomWorkoutDetail customWorkoutDetail);
    }
}
