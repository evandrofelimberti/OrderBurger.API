using FluentValidation;
using OrderBurger.API.DTOs;

namespace OrderBurger.API.Validators;

public class OrderItemValidator: AbstractValidator<OrderItemRequestDTO>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Pedido deve conter pelo menos um produto");
        RuleFor(x => x.Quantity).Equal(1).WithMessage("Quantidade do produto deve ser igual a 1");
    }
    
}