using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly AppGymContextDb _context;

        public WorkoutRepository(AppGymContextDb context)
        {
            _context = context;
        }
        /// <summary>
        /// Faz uma consulta na tabela Workout e busca todos os treinos disponíveis
        /// com a clausula Where, retornando uma lista de objetos Workout.
        /// </summary>       
        /// <returns>Retorna uma lista com todos os treinos disponíveis.</returns>
        public async Task<IEnumerable<Workout>> GetAllWorkoutsAsync()
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe treinos disponíveis.
            /// </sumary>
            try
            {
                return await _context.Workouts
                         .Where(c => c.Active == true)
                         .ToListAsync();
            }
            catch (SqliteException ex)
            {
                if (ex.ErrorCode == 100)
                {
                    throw new Exception("Não há treinos cadastrados.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }

            }
        }
        /// <summary>
        /// Faz uma consulta na tabela Workouts e busca todos os treinos disponíveis
        /// estão as informações adicionais dos treinos personalizados ativos com a clausula Where, retornando um 
        /// objeto Workouts.
        /// </summary>
        /// <param name="id">Id do treino a ser buscado.</param>
        /// <returns>Retorna uma lista com todos os treinos disponíveis.</returns>
        public async Task<Workout> GetWorkoutByIdAsync(int id)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe treino com o id informado.
            /// </sumary>
            try
            {
                return await _context.Workouts
                         .Where(c => c.Active == true && c.Id == id)
                         .FirstOrDefaultAsync();
            }
            catch (SqliteException ex) when (ex.ErrorCode == 2627)
            {
                if (ex.SqliteErrorCode == 100) // Violation of PRIMARY KEY constraint
                {
                    throw new Exception("Treino não encontrado.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }
        }
        /// <summary>
        /// Adiciona um novo treino.
        /// </summary>
        /// <param name="workout">Objeto Workout contendo os detalhes do treino.</param>
        /// <returns>Retorna o objeto Workout adicionado.</returns>
        public async Task<Workout> PostWorkoutAsync(Workout workout)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se os dados foram inseridos com sucesso.
            /// </sumary>
            try
            {
                await _context.Workouts.AddAsync(workout);
                _context.SaveChanges();
                return workout;
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
        /// <summary>
        /// Busca o treino existente com o workoutId e alterar a coluna CustomWorkoutId da Tabela Workouts
        /// para vincular um treino à um treino personalizado.
        /// </summary>
        /// <param name="customWorkoutId">customWorkoutId contendo o id do treino personalizado.</param>
        /// <param name="workoutId">workoutId contendo o id do treino que será vinculado com o treino personalizado.</param>
        /// <returns>Retorna o objeto Workout Alterado.</returns>
        public async Task<Workout> PutWorkoutAsync(int customWorkoutId, int workoutId)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se os dados foram inseridos com sucesso.
            /// </sumary>
            try
            {
               var workout = await _context.Workouts
                            .Where(w => w.Id == workoutId)
                            .FirstOrDefaultAsync();
                workout.CustomWorkoutId = customWorkoutId;
                _context.Entry(workout).Property(e => e.CustomWorkoutId).IsModified = true;

                await _context.SaveChangesAsync();
                return workout;
            }
            catch (SqliteException ex)
            {
                if (ex.SqliteErrorCode == 19)
                {
                    throw new Exception("Erro ao atualizar tabela CustomWorkoutId do Treino.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }

        }
    }
}
