using FluentValidation;
using Pustok.BLL.ViewModels;

namespace Pustok.BLL.Validators;

public class CategoryCreateViewModelValidator : AbstractValidator<CreateCategoryViewModel>
{
    public CategoryCreateViewModelValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(256);
    }
}