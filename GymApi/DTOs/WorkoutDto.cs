using GymApi.Models;

namespace GymApi.DTOs
{
    public class WorkoutDto
    {
        public int Id { get; set; }
        public int UserId { get; set; } //FK
        public DateTime Date { get; set; }

        public string? Name { get; set; }
        public int Duration { get; set; }
        public string? Notes { get; set; }
        public ICollection<WorkoutSetDto>? Sets { get; set; }
    }
}
