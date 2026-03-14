namespace GymApi.DTOs
{
    public class ExerciseUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MuscleGroup { get; set; }
        public string? Equipment { get; set; }
    }
}
