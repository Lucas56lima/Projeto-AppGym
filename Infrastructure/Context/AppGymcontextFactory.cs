using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context
{
    public class AppGymcontextFactory : IDesignTimeDbContextFactory<AppGymContextDb>
    {
        string conectionDefault = "Data Source=C:\\Users\\Usuário\\Downloads\\AppGym\\Infrastructure\\Data\\teste.db";
        string conectionSecondary = "Data Source=C:\\Users\\Lucas Lima\\Source\\Repos\\Lucas56lima\\Project-AppGym\\Infrastructure\\Data\\teste.db";
        public AppGymContextDb CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppGymContextDb>();
            
           
            optionsBuilder.UseSqlite(conectionSecondary) ; // Exemplo de string de conexão SQLite

            return new AppGymContextDb(optionsBuilder.Options);
        }
    }
}