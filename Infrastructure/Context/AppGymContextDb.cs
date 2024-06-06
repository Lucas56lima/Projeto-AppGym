using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Commands;

namespace Infrastructure.Context
{
    public class AppGymContextDb : DbContext
    {
    
        public AppGymContextDb(DbContextOptions<AppGymContextDb> options) : base(options)   
        {        
       
        }
        public DbSet<User> Users { get;set;}
        public DbSet<Plan> Plans { get;set;}
        public DbSet<Payment> Payments { get;set;}
        public DbSet<Workout> Workouts { get;set;}
        public DbSet<WorkoutPersonalized> WorkoutsPersonalizeds {  get;set;}      
       
    }

}