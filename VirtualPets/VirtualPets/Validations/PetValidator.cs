using FluentValidation;
using VirtualPets.DTOs;

namespace VirtualPets.Validations
{
    public class PetValidator : AbstractValidator<PetDTO>
    {
        public PetValidator()
        {
            RuleFor(pet => pet.Name).NotEmpty().MaximumLength(100);
            RuleFor(pet => pet.Species).NotEmpty().MaximumLength(50);
            RuleFor(pet => pet.Breed).NotEmpty().MaximumLength(50);
            RuleFor(pet => pet.Age).GreaterThanOrEqualTo(0);
        }
    }
}
