using FluentValidation;
using Pustok.BLL.ViewModels;

namespace Pustok.BLL.Validators;

public class CategoryUpdateViewModelValidator : AbstractValidator<UpdateCategoryViewModel>
{
    public CategoryUpdateViewModelValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(256);
    }
}
