using FluentValidation;
using OrderBurger.API.DTOs;
using OrderBurger.API.Enums;

namespace OrderBurger.API.Validators;

public sealed class InventoryTransactionValidator : AbstractValidator<InventoryTransactionRequestDTO>
{
    public InventoryTransactionValidator()
    {
        RuleFor(x => x.TransactionType)
            .NotEqual(TransactionType.None).WithMessage("O tipo da movimentação é obrigatório.");

        RuleFor(x => x.BusinessPartnerId)
            .NotEmpty().WithMessage("O parceiro de negócio é obrigatório.");

        RuleFor(x => x.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("O desconto não pode ser negativo.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("A movimentação deve conter ao menos um item.");

        RuleForEach(x => x.Items)
            .SetValidator(new InventoryTransactionItemValidator());
    }
}
