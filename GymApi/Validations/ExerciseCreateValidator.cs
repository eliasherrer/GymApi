using FluentValidation;
using GymApi.DTOs;

namespace GymApi.Validations
{
    public class ExerciseInsertValidator : AbstractValidator<ExerciseInsertDto>
    {
        public ExerciseInsertValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del ejercicio es obligatorio")
                .Length(3, 100).WithMessage("El nombre debe tener entre 3 y 100 caracteres");

            RuleFor(x => x.MuscleGroup)
                .NotEmpty().WithMessage("Debes especificar el grupo muscular (ej. Pecho, Pierna)");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede pasar de 500 caracteres");
        }
    }
}