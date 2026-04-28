using FluentValidation;
using OrderBurger.API.DTOs;
using OrderBurger.API.Models;

namespace OrderBurger.API.Validators;

public class OrderValidator: AbstractValidator<OrderRequestDTO>
{
    public OrderValidator()
    {
        RuleFor(x => x.Items).NotEmpty().WithMessage("Pedido deve conter pelo menos um produto")
            .Must(HaveNoDuplicateProducts).WithMessage("O pedido contém produtos duplicados");
        RuleForEach(x => x.Items).SetValidator(new OrderItemValidator());
    }

    private bool HaveNoDuplicateProducts(IEnumerable<OrderItemRequestDTO> items)
    {
        var duplicatesProductsByQuantity = items.GroupBy(x => x.ProductId).Any(g => g.Count() > 1);
        
        return !duplicatesProductsByQuantity;
    }
    
}