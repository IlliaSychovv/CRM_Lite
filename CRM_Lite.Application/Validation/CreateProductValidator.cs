using CRM_Lite.Application.DTO.Product;
using FluentValidation;

namespace CRM_Lite.Application.Validation;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MinimumLength(2).WithMessage("Product name must be at least 2 characters.");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Category must be a valid product category.");

        RuleFor(x => x.Article)
            .NotEmpty().WithMessage("Article is required.")
            .MinimumLength(2).WithMessage("Article must be at least 2 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}