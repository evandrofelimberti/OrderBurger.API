using FluentValidation;
using OrderBurger.API.DTOs;
using OrderBurger.API.Enums;

namespace OrderBurger.API.Validators;

public sealed class BusinessPartnerValidator : AbstractValidator<BusinessPartnerRequestDTO>
{
    public BusinessPartnerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(150);

        RuleFor(x => x.DocumentNumber)
            .NotEmpty().WithMessage("O número do documento é obrigatório.")
            .MaximumLength(20);

        RuleFor(x => x.DocumentType)
            .NotEqual(DocumentType.None).WithMessage("O tipo de documento é obrigatório.");

        RuleFor(x => x.Type)
            .NotEqual(BusinessPartnerType.None).WithMessage("O tipo de parceiro é obrigatório.");
    }
}
