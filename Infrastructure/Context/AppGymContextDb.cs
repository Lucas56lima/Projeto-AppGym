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
        public DbSet<SpecialUser> SpecialUsers { get;set;}
        public DbSet<Plan> Plans { get;set;}
        public DbSet<Payment> Payments { get;set;}
        public DbSet<Workout> Workouts { get;set;}
        public DbSet<CustomWorkout> CustomWorkouts {  get;set;}
        public DbSet<CustomWorkoutDetail> CustomWorkoutDetails { get; set; }
        public DbSet<Roles>Roles { get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Email, u.Fone })
                .IsUnique();
            modelBuilder.Entity<Plan>()
                .HasIndex(p => new { p.Name, p.Duration })
                .IsUnique();
            modelBuilder.Entity<Workout>()
                .HasIndex(w => new { w.WorkoutName, w.Video })
                .IsUnique();
            modelBuilder.Entity<CustomWorkout>()
                .HasIndex(cw => cw.CustomWorkoutName)
                .IsUnique();
            modelBuilder.Entity<SpecialUser>()
                .HasIndex(su => su.Email)
                .IsUnique();
            modelBuilder.Entity<Roles>()
                .HasIndex(r => r.Name)
                .IsUnique();
            modelBuilder.Entity<CustomWorkoutDetail>()
                .HasIndex(cwd => cwd.Combination)
                .IsUnique();
        }

    }

}