using CRM_Lite.Application.DTO.Customer;
using FluentValidation;

namespace CRM_Lite.Application.Validation;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerDto>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName is required.")
            .MinimumLength(3).WithMessage("FullName must be at least 3 characters.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.")
            .Must(BeAValidIsoDate).WithMessage("BirthDate must be in ISO 8601 format: yyyy-MM-ddTHH:mm:ss.fffZ")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("BirthDate cannot be in the future.");
    }
    
    private bool BeAValidIsoDate(DateTime date)
    {
        var isoString = date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        return DateTime.TryParse(isoString, out _);
    }
}