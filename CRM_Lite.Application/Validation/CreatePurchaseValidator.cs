using CRM_Lite.Application.DTO.Purchase;
using FluentValidation;

namespace CRM_Lite.Application.Validation;

public class CreatePurchaseValidator : AbstractValidator<CreatePurchaseDto>
{
    public CreatePurchaseValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("At least one purchase item is required.");

        RuleForEach(x => x.Items).SetValidator(new PurchaseItemValidator());
    }
    
    private class PurchaseItemValidator : AbstractValidator<PurchaseItemDto>
    {
        public PurchaseItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}