using Domain.Entities;
using Domain.Interface;

namespace Service.Services
{
    public class CustomWorkoutServiceDetailService : ICustomWorkoutDetailService
    {
        private readonly ICustomWorkoutDetailRepository _repository;
        public CustomWorkoutServiceDetailService(ICustomWorkoutDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomWorkoutDetail>> GetAllCustomWorkoutsDeatilsAsync()
        {
            return await _repository.GetAllCustomWorkoutsDeatilsAsync();
        }

        public async Task<CustomWorkoutDetail> GetCustomWorkoutDetailByNameAsync(string name)
        {
            return await _repository.GetCustomWorkoutDetailByNameAsync(name);
        }

        public async Task<CustomWorkoutDetail> PostCustomWorkoutDetailAsync(CustomWorkoutDetail customWorkoutDetail)
        {
            return await _repository.PostCustomWorkoutDetailAsync(customWorkoutDetail);
        }
    }
}
