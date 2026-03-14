using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymApi.Models
{
    public class WorkoutSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        //Relaciones
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }

        public decimal Weight { get; set; }
        public int Reps { get; set; }
        public int OrderIndex { get; set; } //Ordenamiento

        [ForeignKey("WorkoutId")]
        public virtual Workout? Workout { get; set; }

        [ForeignKey("ExerciseId")]
        public virtual Exercise? Exercise { get; set; } 


    }
}
