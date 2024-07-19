using Domain.Entities;
using Domain.Interface;
using Domain.Viewmodel;
using Infrastructure.Context;
using Infrastructure.Handler;
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
                ErrorHandler.HandlerSqliteException(ex);
                return Enumerable.Empty<Workout>();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex);
                return Enumerable.Empty<Workout>();
            }
        }
        /// <summary>
        /// Faz uma consulta na tabela Workouts e uma busca personalizada pelo Id que é uma propriedade
        /// única na tabela utilizandoclausula Where e retornando um objeto Workout.
        /// </summary>
        /// <param name="id">Id do treino a ser buscado.</param>
        /// <returns>Retorna um objeto do treino disponível.</returns>
        public async Task<Workout> GetWorkoutByIdAsync(int id)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe treino com o id informado.
            /// </sumary>
            try
            {
                return await _context.Workouts
                         .Where(c => c.Active == true && c.WorkoutId == id)
                         .FirstOrDefaultAsync();
            }
            catch (SqliteException ex)
            {
                ErrorHandler.HandlerSqliteException(ex);
                return null;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex);
                return null;
            }
        }
        /// <summary>
        /// Faz uma consulta na tabela Workouts e uma busca personalizada pelo Id que é uma propriedade
        /// única na tabela utilizandoclausula Where e retornando um objeto Workout.      
        /// </summary>
        /// <param name="name">Nome do treino a ser buscado.</param>
        /// <returns>Retorna um objeto do treino disponível.</returns>
        public async Task<Workout> GetWorkoutByNameAsync(string name)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe treino com o id informado.
            /// </sumary>
            try
            {
                return await _context.Workouts
                         .Where(c => c.Active == true && c.WorkoutName == name)
                         .FirstOrDefaultAsync();
            }
            catch (SqliteException ex)
            {
                ErrorHandler.HandlerSqliteException(ex);
                return null;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex);
                return null;
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
                ErrorHandler.HandlerSqliteException(ex);
                return null;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex);
                return null;
            }
        }
        /// <summary>
        /// Busca o treino existente pelo Id e altera as informações do objeto na tabela Workouts        
        /// </summary>       
        /// <param name="id">id do treino personalizado que terá suas informações alteradas.</param>
        ///  /// <param name="newWorkout">newWorkout é um objeto do tipo CustomWorkout contendo as informações completas do treino e suas partes que serã
        ///  alteradas.</param>
        /// <returns>retorna o objeto Workout alterado.</returns>
        public async Task<Workout> PutWorkoutAsync(int id, Workout newWorkout)
        {
            ///<summary>
            ///Try valida se a conexão é válida ou se os dados foram inseridos com sucesso.
            /// </summary>
            try
            {
                var workout = await GetWorkoutByIdAsync(id);                

                // Atualize os valores sem alterar a chave primária
                _context.Entry(workout).CurrentValues.SetValues(newWorkout);                

                _context.Entry(workout).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return workout;
            }
            catch (SqliteException ex)
            {
                ErrorHandler.HandlerSqliteException(ex);
                return null;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleException(ex);
                return null;
            }            
        }
    }
}
