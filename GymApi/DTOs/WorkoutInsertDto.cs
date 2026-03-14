namespace GymApi.DTOs
{
    public class WorkoutInsertDto
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
    }
}
