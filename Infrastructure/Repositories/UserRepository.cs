using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Infrastructure.Handler;
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
        /// Faz uma consulta na tabela Users e busca todos os usu�rios dispon�veis
        /// com a clausula Where, retornando uma 
        /// lista de objetos User.
        /// </summary>       
        /// <returns>Retorna uma lista com todos os usu�rios dispon�veis.</returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            ///<sumary>
            ///Try valida se a conex�o � v�lida ou se existe usu�rios dispon�veis.
            /// </sumary>
            try
            {            
                return await _context.Users
                            .Where(c => c.Active ==  true)
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
        /// Faz uma consulta na tabela Users e busca todos os treinos dispon�veis
        /// est�o as informa��es adicionais dos usu�rios ativos com a clausula Where, retornando um 
        /// objeto User.
        /// </summary>
        /// <param name="email">Email do usu�rio a ser buscado.</param>
        /// <returns>Retorna um objeto com o usu�rio correspondente.</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            ///<sumary>
            ///Try valida se a conex�o � v�lida ou se existe usu�rio dispon�vel.
            /// </sumary>
            try
            {
                return await _context.Users
                        .Where(u => u.Email == email && u.Active == true)
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
        /// Faz uma consulta na tabela Users e busca todos os usu�rios dispon�veis
        /// est�o as informa��es adicionais dos usu�rios ativos com a clausula Where, retornando um 
        /// objeto User.
        /// </summary>
        /// <param name="id">Id do usu�rio a ser buscado.</param>
        /// <returns>Retorna um objeto com o usu�rio correspondente.</returns>

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
        /// Adiciona um novo usu�rio.
        /// </summary>
        /// <param name="user">Objeto User contendo os detalhes do usu�rio.</param>
        /// <returns>Retorna o objeto User adicionado.</returns>
        public async Task<User> PostUserAsync(User user)
        {
            ///<sumary>
            ///Try valida se a conex�o � v�lida ou se os dados foram inseridos com sucesso.
            /// </sumary>
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
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
        /// Adiciona um novo Usu�rio Admin.
        /// </summary>
        /// <param name="specialUser">Objeto User contendo os detalhes do Usu�rio de Role Admin.</param>
        /// <returns>Retorna o objeto Usu�rio adicionado.</returns>
        /// </sumary>
        public async Task<User> PostSpecialUserAdminAsync(User specialUser)
        {
            
            try
            {
                await _context.Users.AddAsync(specialUser);
                await _context.SaveChangesAsync();
                return specialUser;
            }
            catch (SqliteException ex)
            {
                if (ex.SqliteErrorCode == 19)
                {
                    throw new Exception("Erro ao inserir usu�rio.", ex);
                }
                else
                {
                    throw new Exception("Erro ao acessar o banco de dados.", ex);
                }
            }
        }
        /// <summary>
        /// Adiciona um novo Usu�rio Super.
        /// </summary>
        /// <param name="specialUser">Objeto User contendo os detalhes do Usu�rio de Role Super.</param>
        /// <returns>Retorna o objeto Usu�rio adicionado.</returns>
        public async Task<User> PostSpecialUserSuperAsync(User specialUser)
        {
            try
            {
                await _context.Users.AddAsync(specialUser);
                await _context.SaveChangesAsync();
                return specialUser;
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