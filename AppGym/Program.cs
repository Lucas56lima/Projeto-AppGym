using Domain.Interface;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("ConectionDefault");
var conectionSecundary = builder.Configuration.GetConnectionString("ConectionSecundary");
Console.WriteLine($"String de conexão: {connectionString}");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'DefaultConnection' não está configurada.");
}

builder.Services.AddDbContext<AppGymContextDb>(options =>
    options.UseSqlite(connectionString));    

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<ICustomWorkoutRepository, CustomWorkoutRepository>();
builder.Services.AddScoped<ICustomWorkoutService, CustomWorkoutService>();
builder.Services.AddScoped<ICustomWorkoutDetailRepository, CustomWorkoutDetailRepository>();
builder.Services.AddScoped<ICustomWorkoutDetailService, CustomWorkoutDetailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();
app.Run();

