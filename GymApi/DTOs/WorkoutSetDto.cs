namespace GymApi.DTOs
{
    public class WorkoutSetDto
    {
        public int Id { get; set; }
        //Relaciones
        public int WorkoutId { get; set; }
        public string? ExerciseName { get; set; }
        public decimal Weight { get; set; }
        public int Reps { get; set; }
        public int OrderIndex { get; set; }
    }
}
