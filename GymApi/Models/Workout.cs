using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GymApi.Models
{
    public class Workout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; } //FK
        public DateTime Date { get; set; }

        public string? Name  { get; set; }
        public int Duration { get; set; }
        public string? Notes { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public virtual ICollection<WorkoutSet>? Sets { get; set; } = new List<WorkoutSet>();
    }
}
