using Domain.Entities;
using Domain.Viewmodel;

namespace Domain.Interface
{
    public interface ICustomWorkoutDetailService
    {
        Task<CustomWorkoutViewModel> GetCustomWorkoutDetailByNameAsync(string name);
        //Task<CustomWorkoutDetail> GetCustomWorkoutDetailByCustomWorkoutId(int customWorkoutId);
        Task<IEnumerable<CustomWorkoutDetailViewModel>> GetAllCustomWorkoutsDetailsAsync();
        Task<CustomWorkoutDetail> PostCustomWorkoutDetailAsync(CustomWorkoutDetail customWorkoutDetail);        
    }
}
