namespace GymApi.DTOs
{
    public class WorkoutSetInsertDto
    {
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public decimal Weight { get; set; }
        public int Reps { get; set; }
    }
}
