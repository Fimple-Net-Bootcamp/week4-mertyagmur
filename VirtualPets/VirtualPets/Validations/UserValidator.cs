using FluentValidation;
using VirtualPets.DTOs;

namespace VirtualPets.Validations
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username).NotEmpty().MaximumLength(50);
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.Password).NotEmpty();
        }
    }
}
