using FluentValidation;
using VirtualPets.DTOs;

namespace VirtualPets.Validations
{
    public class ActivityValidator : AbstractValidator<ActivityDTO>
    {
        public ActivityValidator()
        {
            RuleFor(activity => activity.Name).NotEmpty().MaximumLength(100);
            RuleFor(activity => activity.Type).NotEmpty().MaximumLength(50);
            RuleFor(activity => activity.HungerImpact).InclusiveBetween(0, 100);
            RuleFor(activity => activity.HappinessImpact).InclusiveBetween(0, 100);
            RuleFor(activity => activity.CleanlinessImpact).InclusiveBetween(0, 100);
        }
    }
}
