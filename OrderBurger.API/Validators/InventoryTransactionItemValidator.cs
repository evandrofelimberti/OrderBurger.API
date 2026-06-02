using FluentValidation;
using OrderBurger.API.DTOs;

namespace OrderBurger.API.Validators;

public sealed class InventoryTransactionItemValidator : AbstractValidator<InventoryTransactionItemRequestDTO>
{
    public InventoryTransactionItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("O produto é obrigatório.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
    }
}
