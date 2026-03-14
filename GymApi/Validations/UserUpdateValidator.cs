using FluentValidation;
using GymApi.DTOs;

namespace GymApi.Validations
{
    public class UserUpdateValidator :AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator() 
        {
            RuleFor(x=> x.Name).NotEmpty().Length(1,50).WithMessage("El nombre debe medir entre 2 y 50 caracteres");
        }
    }
}
