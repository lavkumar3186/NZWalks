using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class UpdateRegionRequestValidator : AbstractValidator<UpdateRegionRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(u => u.Code).NotEmpty();
            RuleFor(u => u.Name).NotEmpty();
            RuleFor(u => u.Area).GreaterThan(0);
            RuleFor(u => u.Population).GreaterThanOrEqualTo(0);

        }
    }
}
