using FluentValidation;
using OrderBurger.API.DTOs;
using OrderBurger.API.Models;

namespace OrderBurger.API.Validators;

public class OrderItemValidator: AbstractValidator<OrderItemRequestDTO>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Pedido deve conter pelo menos um produto");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantidade do produto deve ser maior que zero");
    }
    
}