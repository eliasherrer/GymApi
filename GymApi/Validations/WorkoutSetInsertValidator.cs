using FluentValidation;
using GymApi.DTOs;

namespace GymApi.Validations
{
    public class WorkoutSetInsertValidator : AbstractValidator<WorkoutSetInsertDto>
    {
        public WorkoutSetInsertValidator()
        {
            RuleFor(x => x.WorkoutId).GreaterThan(0).WithMessage("Debe pertenecer a una rutina válida");
            RuleFor(x => x.ExerciseId).GreaterThan(0).WithMessage("Debe tener un ejercicio asignado");
            RuleFor(x => x.Weight).GreaterThanOrEqualTo(0).WithMessage("El peso no puede ser negativo");
            RuleFor(x => x.Reps).GreaterThan(0).WithMessage("Debes hacer al menos una repetición");
        }
    }
}