using FluentValidation;
using GordinhosFelizes.Application.DTOs.Create;

namespace GordinhosFelizes.Application.Validators;

public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewValidator()
    {
        RuleFor(x => x.RestaurantId)
            .GreaterThan(0);

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);

        RuleFor(x => x.Comment)
            .NotEmpty()
            .MaximumLength(500);
    }
}
