using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymApi.Models
{
    public class Exercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? MuscleGroup { get; set; }
        public string? Description { get; set; }
        public string? Equipment { get; set; }

    }
}
