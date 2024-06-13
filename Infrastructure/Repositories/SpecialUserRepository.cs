using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SpecialUserRepository : ISpecialUserRepository
    {
        private readonly AppGymContextDb _context;

        public SpecialUserRepository(AppGymContextDb context)
        {
            _context = context;
        }       

        public async Task<SpecialUser> GetSpecialUserByEmailAsync(string email)
        {
            ///<sumary>
            ///Try valida se a conexão é válida ou se existe usuário disponível.
            /// </sumary>
            try
            {
                return await _context.SpecialUsers
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

        public async Task<SpecialUser> PostSpecialUserAdminAsync(SpecialUser specialUser)
        {
            /// </sumary>
            try
            {
                await _context.SpecialUsers.AddAsync(specialUser);
                await _context.SaveChangesAsync();
                return specialUser;
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

        public async Task<SpecialUser> PostSpecialUserSuperAsync(SpecialUser specialUser)
        {
            try
            {
                await _context.SpecialUsers.AddAsync(specialUser);
                await _context.SaveChangesAsync();
                return specialUser;
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
