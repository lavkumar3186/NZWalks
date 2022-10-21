using FluentValidation;
using NZWalks.API.Models.DTO.WalkDifficulty;

namespace NZWalks.API.Validators
{
    public class AddWalkDifficultyRequestValidator : AbstractValidator<WalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            RuleFor(w => w.Code).NotEmpty();
        }
    }
}
