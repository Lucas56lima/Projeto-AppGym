using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
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
            catch(SqliteException ex)
            {
                if (ex.ErrorCode == 100)
                {
                    throw new Exception ("Não há treinos personalizados cadastrados.",ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }

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
            catch (SqliteException ex) when (ex.ErrorCode == 2627)
            {
                if (ex.SqliteErrorCode == 100) // Violation of PRIMARY KEY constraint
                {
                    throw new Exception("Treino personalizado não encontrado.", ex);
                }               
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
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
            catch(SqliteException ex)
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
