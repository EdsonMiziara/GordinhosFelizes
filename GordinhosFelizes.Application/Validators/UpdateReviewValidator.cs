using FluentValidation;
using GordinhosFelizes.Application.DTOs.Update;

namespace GordinhosFelizes.Application.Validators;

public class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
{
    public UpdateReviewValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);

        RuleFor(x => x.Comment)
            .NotEmpty()
            .MaximumLength(500);
    }
}
