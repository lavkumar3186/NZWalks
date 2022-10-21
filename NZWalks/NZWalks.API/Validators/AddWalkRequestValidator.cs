using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class AddWalkRequestValidator : AbstractValidator<AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(w => w.Name).NotEmpty();
            RuleFor(w => w.Length).GreaterThan(0);
        }
    }
}
