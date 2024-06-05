using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Context
{
    public class AppGymContextDb : DbContext
    {
    
        public AppGymContextDb(DbContextOptions<AppGymContextDb> options) : base(options)   
        {        
       
        }
         public DbSet<User> Users { get;set;}
       
    }

}