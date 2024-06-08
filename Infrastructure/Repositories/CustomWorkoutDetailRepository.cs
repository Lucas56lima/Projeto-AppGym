using Domain.Entities;
using Domain.Interface;
using Domain.Viewmodel;
using Infrastructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomWorkoutDetailRepository : ICustomWorkoutDetailRepository
    {
        private readonly AppGymContextDb _context;
        private readonly IWorkoutRepository _workoutRepository;
        public CustomWorkoutDetailRepository(AppGymContextDb context, IWorkoutRepository workoutRepository)
        {
            _context = context;
            _workoutRepository = workoutRepository;
        }

        /// <summary>
        /// Faz uma consulta utilizando inner join partindo da Tabela Workout onde estão
        /// as informações do cadastro de treino, seguido da CustomWorkout onde estão as informações 
        /// dos Treinos personalizados, e finalizando na CustomWorkoutDetails onde
        /// estão as informações adicionais dos treinos, retornando uma lista de objetos CustomWorkoutDetailViewModel.
        /// </summary>
        /// <returns>Retorna uma lista com todos os treinos disponíveis.</returns>
        public async Task<IEnumerable<CustomWorkoutDetailViewModel>> GetAllCustomWorkoutsDetailsAsync()
        {
            try
            {
                return await _context.Workouts
                                     .Join(_context.CustomWorkouts,
                                           workout => workout.CustomWorkoutId,
                                           customWorkout => customWorkout.Id,
                                           (workout, customWorkout) => new { workout, customWorkout })
                                     .Join(_context.CustomWorkoutDetails,
                                           combined => combined.customWorkout.Id,
                                           customWorkoutDetail => customWorkoutDetail.CustomWorkoutId,
                                           (combined, customWorkoutDetail) => new CustomWorkoutDetailViewModel
                                           {
                                               Name = combined.workout.Name,
                                               MuscleGroup = combined.workout.MuscleGroup,
                                               Description = combined.workout.Description,
                                               TrainningPlace = combined.workout.TrainningPlace,
                                               CustomWorkoutName = combined.customWorkout.Name,
                                               Finally = combined.customWorkout.Finally,
                                               Repetitions = customWorkoutDetail.Repetitions,
                                               Time = customWorkoutDetail.Time,
                                               Interval = customWorkoutDetail.Interval,
                                               Sequence = customWorkoutDetail.Sequence
                                           }).ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Erro ao recuperar os treinos do banco de dados", ex);
            }
        }

        /// <summary>
        /// Faz uma consulta utilizando inner join partindo da Tabela Workout onde estão
        /// as informações do cadastro de treino, seguido da CustomWorkout onde estão as informações 
        /// dos Treinos personalizados, e finalizando na CustomWorkoutDetails onde
        /// estão as informações adicionais dos treinos, utilizando a cláusula where com o parâmetro name.
        /// </summary>
        /// <param name="name">Nome do treino personalizado.</param>
        /// <returns>Retorna um objeto CustomWorkoutDetailViewModel correspondente ao nome fornecido.</returns>
        public async Task<CustomWorkoutDetailViewModel> GetCustomWorkoutDetailByNameAsync(string name)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe treinos com o nome informado.
            /// </sumary>
            try
            {
                return await _context.Workouts
                                     .Join(_context.CustomWorkouts,
                                           workout => workout.CustomWorkoutId,
                                           customWorkout => customWorkout.Id,
                                           (workout, customWorkout) => new { workout, customWorkout })
                                     .Join(_context.CustomWorkoutDetails,
                                           combined => combined.customWorkout.Id,
                                           customWorkoutDetail => customWorkoutDetail.CustomWorkoutId,
                                           (combined, customWorkoutDetail) => new CustomWorkoutDetailViewModel
                                           {
                                               Name = combined.workout.Name,
                                               MuscleGroup = combined.workout.MuscleGroup,
                                               Description = combined.workout.Description,
                                               TrainningPlace = combined.workout.TrainningPlace,
                                               CustomWorkoutName = combined.customWorkout.Name,
                                               Finally = combined.customWorkout.Finally,
                                               Repetitions = customWorkoutDetail.Repetitions,
                                               Time = customWorkoutDetail.Time,
                                               Interval = customWorkoutDetail.Interval,
                                               Sequence = customWorkoutDetail.Sequence
                                           })
                                     .Where(c => c.CustomWorkoutName == name)
                                     .FirstOrDefaultAsync();
            }
            catch (SqliteException ex)
            {
                if (ex.ErrorCode == 100)
                {
                    throw new Exception("Não há treinos personalizados cadastrados.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }
        }

        /// <summary>
        /// Adiciona um novo treino personalizado detalhado com as informações dos treinos e do treino personalizado.
        /// </summary>
        /// <param name="customWorkoutDetail">Objeto CustomWorkoutDetail contendo os detalhes do treino.</param>
        /// <returns>Retorna o objeto CustomWorkoutDetail adicionado.</returns>
        public async Task<CustomWorkoutDetail> PostCustomWorkoutDetailAsync(CustomWorkoutDetail customWorkoutDetail)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se os dados foram inseridos com sucesso.
            /// </sumary>
            try
            {
                await _context.AddAsync(customWorkoutDetail);
                await _workoutRepository.PutWorkoutAsync(customWorkoutDetail.CustomWorkoutId, customWorkoutDetail.WorkoutId);
                await _context.SaveChangesAsync();
                return customWorkoutDetail;
            }
            catch (SqliteException ex)
            {
                if (ex.SqliteErrorCode == 19)
                {
                    throw new Exception("Erro ao inserir Treino personalizado.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }            
        }
    }
}
