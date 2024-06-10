using Domain.Entities;
using Domain.Viewmodel;

namespace Domain.Interface
{
    public interface ICustomWorkoutDetailRepository
    {
        Task<CustomWorkoutViewModel> GetCustomWorkoutDetailByNameAsync(string name);
        Task<IEnumerable<CustomWorkoutDetailViewModel>> GetAllCustomWorkoutsDetailsAsync();
        Task<CustomWorkoutDetail> PostCustomWorkoutDetailAsync(CustomWorkoutDetail customWorkoutDetail);
    }
}
