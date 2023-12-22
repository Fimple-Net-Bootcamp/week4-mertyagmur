using FluentValidation;
using VirtualPets.DTOs;

namespace VirtualPets.Validations
{
    public class HealthValidator : AbstractValidator<HealthDTO>
    {
        public HealthValidator()
        {
            RuleFor(health => health.Hunger).InclusiveBetween(0, 100);
            RuleFor(health => health.Happiness).InclusiveBetween(0, 100);
            RuleFor(health => health.Cleanliness).InclusiveBetween(0, 100);
        }
    }
}
