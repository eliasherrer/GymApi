using FluentValidation;
using GymApi.Automappers;
using GymApi.Context;
using GymApi.DTOs;
using GymApi.Models;
using GymApi.Repository;
using GymApi.Services;
using GymApi.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Entity Framework
builder.Services.AddDbContext<GymContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GymConnection"));
});

//Servicios
builder.Services.AddKeyedScoped<ICommonService<UserDto, UserInsertDto, UserUpdateDto>, UserService>("UserService");
builder.Services.AddKeyedScoped<ICommonService<ExerciseDto, ExerciseInsertDto, ExerciseUpdateDto>, ExerciseService>("ExerciseService");
builder.Services.AddKeyedScoped<ICommonService<WorkoutDto, WorkoutInsertDto, WorkoutUpdateDto>, WorkoutService>("WorkoutService");
builder.Services.AddKeyedScoped<ICommonService<WorkoutSetDto, WorkoutSetInsertDto, WorkoutSetUpdateDto>, WorkoutSetService>("WorkoutSetService");
//builder.Services.AddScoped<IExerciseService, ExerciseService>();
//builder.Services.AddScoped<IWorkoutService, WorkoutService>();
//builder.Services.AddScoped<IWorkoutSetService, WorkoutSetService>();
//Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(CommonRepository<>));

//Validaciones
builder.Services.AddScoped<IValidator<UserInsertDto>, UserInsertValidator>();
builder.Services.AddScoped<IValidator<UserUpdateDto>, UserUpdateValidator>();
builder.Services.AddScoped<IValidator<WorkoutInsertDto>, WorkoutInsertValidator>();
//builder.Services.AddScoped<IValidator<WorkoutUpdateDto>, WorkoutUpdateValidator>();
builder.Services.AddScoped<IValidator<WorkoutSetInsertDto>, WorkoutSetInsertValidator>();
builder.Services.AddScoped<IValidator<ExerciseInsertDto>, ExerciseInsertValidator>();



//Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
