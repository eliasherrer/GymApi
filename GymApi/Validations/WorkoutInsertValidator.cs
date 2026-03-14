using FluentValidation;
using GymApi.DTOs;

namespace GymApi.Validations
{
    public class WorkoutInsertValidator : AbstractValidator<WorkoutInsertDto>
    {
        public WorkoutInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre de la rutina es obligatorio");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("El ID de usuario no es válido");
            RuleFor(x => x.Date).NotEmpty().WithMessage("La fecha es obligatoria");
        }
    }
}