using GymApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GymApi.Context
{
    public class GymContext : DbContext
    {
        public GymContext(DbContextOptions<GymContext> options) : base(options) { }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutSet> workoutSets { get; set; }
    }
}
