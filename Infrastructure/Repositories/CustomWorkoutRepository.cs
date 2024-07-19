using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Infrastructure.Handler;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomWorkoutRepository : ICustomWorkoutRepository
    {
        private readonly AppGymContextDb _context;
        public CustomWorkoutRepository(AppGymContextDb context)
        {
            _context = context;
        }
        /// <summary>
        /// Faz uma consulta na tabela CustomWorkouts e busca todos os treinos disponíveis
        /// com a clausula Where, retornando uma lista de objetos CustomWorkout.
        /// </summary>       
        /// <returns>Retorna uma lista com todos os treinos disponíveis.</returns>
        public async Task<IEnumerable<CustomWorkout>> GetAllCustomWorkoutsAsync()
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe treinos disponíveis.
            /// </sumary>
            try
            {
                return await _context.CustomWorkouts
                         .Where(c => c.Active == true)
                         .ToListAsync();
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
        /// Faz uma consulta na tabela CustomWorkouts e busca todos os treinos disponíveis
        /// estão as informações adicionais dos treinos personalizados ativos com a clausula Where, retornando um 
        /// objeto CustomWorkout.
        /// </summary>
        /// <param name="id">Id do treino a ser buscado.</param>
        /// <returns>Retorna uma lista com todos os treinos disponíveis.</returns>

        public async Task<CustomWorkout> GetCustomWorkoutByIdAsync(int id)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe treino com o id informado.
            /// </sumary>
            try
            {
                return await _context.CustomWorkouts
                         .Where(c => c.Active == true && c.CustomWorkoutId == id)
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
        /// Faz uma consulta na tabela CustomWorkouts e busca todos os treinos disponíveis
        /// estão as informações adicionais dos treinos personalizados ativos com a clausula Where, retornando um 
        /// objeto CustomWorkout.
        /// </summary>
        /// <param name="name">nome do treino a ser buscado.</param>
        /// <returns>Retorna uma lista com todos os treinos disponíveis.</returns>
        public async Task<CustomWorkout> GetCustomWorkoutByNameAsync(string name)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe treino com o id informado.
            /// </sumary>
            try
            {
                return await _context.CustomWorkouts
                         .Where(c => c.Active == true && c.CustomWorkoutName == name)
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
        /// Adiciona um novo treino personalizado.
        /// </summary>
        /// <param name="customWorkout">Objeto CustomWorkout contendo os detalhes do treino.</param>
        /// <returns>Retorna o objeto CustomWorkout adicionado.</returns>
        public async Task<CustomWorkout> PostCustomWorkoutAsync(CustomWorkout customWorkout)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se os dados foram inseridos com sucesso.
            /// </sumary>
            try
            {
                await _context.CustomWorkouts.AddAsync(customWorkout);
                _context.SaveChanges();
                return customWorkout;
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
        /// Busca o treino personalizado existente pelo Id e altera as informações do objeto na tabela CustomWorkouts        
        /// </summary>       
        /// <param name="id">id do treino personalizado que terá suas informações alteradas.</param>
        /// <param name="newCustomWorkout">newCustomWorkout é um objeto do tipo CustomWorkout contendo as informações completas do treino e suas partes que serã
        ///  alteradas.</param>
        /// <returns>retorna o objeto CustomWorkout alterado.</returns>
        public async Task<CustomWorkout> PutCustomWorkoutAsync(int id, CustomWorkout newCustomWorkout)
        {
            ///<summary>
            ///Try valida se a conexão é válida ou se os dados foram inseridos com sucesso.
            /// </summary>
            try
            {
                var customWorkout = await GetCustomWorkoutByIdAsync(id);

                _context.Entry(customWorkout).CurrentValues.SetValues(newCustomWorkout);
                _context.Entry(customWorkout).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return customWorkout;
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
        /// Altera todos as linhas cujo a coluna CustomWorkoutId é igual ao parãmetro id tornando o treino personalizado inativo.
        /// </summary>
        /// <param name="id"> customWorkoutId contendo o id do treino personalizado.</param>
        /// <returns>Retorna uma string "treino inativo" informando sucesso. </returns>
        public async Task<string> PutCustomWorkoutInTableDetailsAsync(int id)
        {
            ///<summary>
            ///Try valida se a conexão é válida ou se os dados foram inseridos com sucesso.
            /// </summary>
            try
            {
                await _context.CustomWorkoutDetails
                        .Where(c => c.WorkoutId == id)
                        .UpdateFromQueryAsync(c => new CustomWorkoutDetail { Active = false });
                return "treino inativo";

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
