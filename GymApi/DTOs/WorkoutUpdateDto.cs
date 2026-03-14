namespace GymApi.DTOs
{
    public class WorkoutUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string? Notes { get; set; }
    }
}
