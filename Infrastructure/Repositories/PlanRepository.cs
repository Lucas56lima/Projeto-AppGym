using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly AppGymContextDb _context;
        public PlanRepository(AppGymContextDb context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Plan>> GetAllPlansAsync()
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe planos disponíveis.
            /// </sumary>
            try
            {
                return await _context.Plans
                            .Where(c => c.Active == true)
                            .ToListAsync();
            }
            catch (SqliteException ex)
            {
                if (ex.ErrorCode == 100)
                {
                    throw new Exception("Não há planos cadastrados.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }
        }
        /// <summary>
        /// Faz uma consulta na tabela Plans e busca todos os planos disponíveis
        /// estão as informações adicionais dos planos ativos com a clausula Where, retornando um 
        /// objeto Plan.
        /// </summary>
        /// <param name="id">Id do plano a ser buscado.</param>
        /// <returns>Retorna um objeto com o plano correspondente.</returns>

        public async Task<Plan> GetPlanByIdAsync(int id)
        {
            try
            {
                return await _context.Plans
                            .Where(u => u.Id == id && u.Active == true)
                            .FirstOrDefaultAsync();
            }
            catch (SqliteException ex)
            {
                if (ex.ErrorCode == 100)
                {
                    throw new Exception("Não há plano disponível.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }
        }
        /// <summary>
        /// Faz uma consulta na tabela Plans e busca todos os planos disponíveis
        /// estão as informações adicionais dos planos ativos com a clausula Where, retornando um 
        /// objeto Plan.
        /// </summary>
        /// <param name="name">Id do plano a ser buscado.</param>
        /// <returns>Retorna um objeto com o plano correspondente.</returns>
        public async Task<Plan> GetPlanByNameAsync(string name)
        {
            try
            {
                return await _context.Plans
                            .Where(u => u.Name == name && u.Active == true)
                            .FirstOrDefaultAsync();
            }
            catch (SqliteException ex)
            {
                if (ex.ErrorCode == 100)
                {
                    throw new Exception("Não há plano disponível.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }
        }
        /// <summary>
        /// Adiciona um novo plano.
        /// </summary>
        /// <param name="plan">Objeto Plan contendo os detalhes do Plano.</param>
        /// <returns>Retorna o objeto plan adicionado.</returns>
        public async Task<Plan> PostPlanAsync(Plan plan)
        {
            try
            {
                await _context.Plans.AddAsync(plan);
                await _context.SaveChangesAsync();
                return plan;
            }
            catch (SqliteException ex)
            {
                if (ex.SqliteErrorCode == 19)
                {
                    throw new Exception("Erro ao inserir usuário.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }
        }

        public Task<Plan> PutPlanByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
