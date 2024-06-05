using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context
{
    public class AppGymcontextFactory : IDesignTimeDbContextFactory<AppGymContextDb>
    {

        public AppGymContextDb CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppGymContextDb>();
            
            optionsBuilder.UseSqlite("Data Source=Data/test.db"); // Exemplo de string de conexão SQLite

            return new AppGymContextDb(optionsBuilder.Options);
        }
    }
}