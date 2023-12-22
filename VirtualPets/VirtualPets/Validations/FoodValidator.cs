using FluentValidation;
using VirtualPets.DTOs;

namespace VirtualPets.Validations
{
    public class FoodValidator : AbstractValidator<FoodDTO>
    {
        public FoodValidator()
        {
            RuleFor(food => food.Name).NotEmpty().MaximumLength(100);
            RuleFor(food => food.HungerImpact).InclusiveBetween(0, 100);
            RuleFor(food => food.HappinessImpact).InclusiveBetween(0, 100);
        }
    }
}
