using Domain.Entities;
using Domain.Interface;
using Domain.Viewmodel;

namespace Service.Services
{
    public class CustomWorkoutDetailService : ICustomWorkoutDetailService
    {
        private readonly ICustomWorkoutDetailRepository _repository;
        public CustomWorkoutDetailService(ICustomWorkoutDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomWorkoutDetailViewModel>> GetAllCustomWorkoutsDetailsAsync()
        {
            return await _repository.GetAllCustomWorkoutsDetailsAsync();
        }        

        public async Task<CustomWorkoutViewModel> GetCustomWorkoutDetailByNameAsync(string name)
        {
            return await _repository.GetCustomWorkoutDetailByNameAsync(name);
        }

        public async Task<CustomWorkoutDetail> PostCustomWorkoutDetailAsync(CustomWorkoutDetail customWorkoutDetail)
        {                    
           
            return await _repository.PostCustomWorkoutDetailAsync(customWorkoutDetail);
        }
        
    }
}
