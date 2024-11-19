using FluentValidation;
using Pustok.BLL.ViewModels;

namespace Pustok.BLL.Validators;

public class CreateProductViewModelValidator : AbstractValidator<CreateProductViewModel>
{
    public CreateProductViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Cannot be empty").MaximumLength(255).WithMessage("Lenght should be less than 255");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Cannot be empty").MaximumLength(1024).WithMessage("Lenght should be less than 1024");
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Cannot be empty").MaximumLength(100).WithMessage("Lenght should be less than 100");
        RuleFor(x => x.ProductCode).NotEmpty().WithMessage("Cannot be empty").MaximumLength(100).WithMessage("Lenght should be less than 100");
        RuleFor(x => x.OriginalPrice).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");
        RuleFor(x => x.DiscountPrice).GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative")
            .LessThanOrEqualTo(100).WithMessage("Discount percentage cannot be creater than 100");
        RuleFor(x => x.ExTax).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");
        RuleFor(x => x.RewardPoint).GreaterThanOrEqualTo(0).WithMessage("Cannot be negative");
        RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative");
        RuleFor(x => x.MainImage).NotEmpty().SetValidator(new FileValidator());
        RuleFor(x => x.HoverImage).NotEmpty().SetValidator(new FileValidator());
        RuleFor(x => x.AdditionalImages).NotEmpty();
        RuleForEach(x => x.AdditionalImages).SetValidator(new FileValidator());
    }
}
