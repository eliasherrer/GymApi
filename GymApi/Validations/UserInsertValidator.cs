using FluentValidation;
using GymApi.DTOs;

namespace GymApi.Validations
{
    public class UserInsertValidator : AbstractValidator<UserInsertDto>
    {
        public UserInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio").Length(2, 50).WithMessage("El nombre debe medir entre 2 y 50 caracteres");

            RuleFor(x => x.Email).NotEmpty().WithMessage("El correo es obligatorio").EmailAddress().WithMessage("El formato del correo es incorrecto");

            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es obligatoria").MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres");
        }
    }
}