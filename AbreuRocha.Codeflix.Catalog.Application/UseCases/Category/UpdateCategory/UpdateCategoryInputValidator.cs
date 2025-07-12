using FluentValidation;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
public class UpdateCategoryInputValidator
    : AbstractValidator<UpdateCategoryInput>
{
    public UpdateCategoryInputValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must not be empty");
        //RuleFor(x => x.Name)
        //    .NotEmpty()
        //    .MinimumLength(3)
        //    .MaximumLength(255)
        //    .WithMessage("Name should be at least 3 characters long and less or equal 255 characters long");
        //RuleFor(x => x.Description)
        //    .NotNull()
        //    .MaximumLength(10_000)
        //    .WithMessage("Description should not be null and less or equal 10000 characters long");
    }
}
