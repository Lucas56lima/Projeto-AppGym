using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppGymContextDb _context;

        public UserRepository(AppGymContextDb context){
            _context = context;
        }
        /// <summary>
        /// Faz uma consulta na tabela Users e busca todos os usuários disponíveis
        /// com a clausula Where, retornando uma 
        /// lista de objetos User.
        /// </summary>       
        /// <returns>Retorna uma lista com todos os usuários disponíveis.</returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe usuários disponíveis.
            /// </sumary>
            try
            {            
                return await _context.Users
                            .Where(c => c.Active ==  true)
                            .ToListAsync();
            }
            catch (SqliteException ex)
            {
                if (ex.ErrorCode == 100)
                {
                    throw new Exception("Não há usuários cadastrados.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }
        }
        /// <summary>
        /// Faz uma consulta na tabela Users e busca todos os treinos disponíveis
        /// estão as informações adicionais dos usuários ativos com a clausula Where, retornando um 
        /// objeto User.
        /// </summary>
        /// <param name="email">Email do usuário a ser buscado.</param>
        /// <returns>Retorna um objeto com o usuário correspondente.</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe usuário disponível.
            /// </sumary>
            try
            {
                return await _context.Users
                        .Where(u => u.Email == email && u.Active == true)
                        .FirstOrDefaultAsync();
            }
            catch (SqliteException ex)
            {
                if (ex.ErrorCode == 100)
                {
                    throw new Exception("Não há usuários cadastrados.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }
        }
        /// <summary>
        /// Faz uma consulta na tabela Users e busca todos os treinos disponíveis
        /// estão as informações adicionais dos usuários ativos com a clausula Where, retornando um 
        /// objeto User.
        /// </summary>
        /// <param name="id">Id do usuário a ser buscado.</param>
        /// <returns>Retorna um objeto com o usuário correspondente.</returns>

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                return await _context.Users
                            .Where(u => u.Id == id && u.Active == true)
                            .FirstOrDefaultAsync();
            }
            catch (SqliteException ex)
            {
                if (ex.ErrorCode == 100)
                {
                    throw new Exception("Não há usuários cadastrados.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }

        }
        /// <summary>
        /// Adiciona um novo usuário.
        /// </summary>
        /// <param name="user">Objeto User contendo os detalhes do usuário.</param>
        /// <returns>Retorna o objeto User adicionado.</returns>
        public async Task<User> PostUserAsync(User user)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se os dados foram inseridos com sucesso.
            /// </sumary>
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
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
        
    }
}