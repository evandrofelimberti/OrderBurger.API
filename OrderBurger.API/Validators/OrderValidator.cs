using FluentValidation;
using OrderBurger.API.DTOs;

namespace OrderBurger.API.Validators;

public class OrderValidator: AbstractValidator<OrderRequestDTO>
{
    public OrderValidator()
    {
        RuleFor(x => x.Items).NotEmpty().WithMessage("Pedido deve conter pelo menos um produto");
        RuleForEach(x => x.Items).SetValidator(new OrderItemValidator());
    }
    
}