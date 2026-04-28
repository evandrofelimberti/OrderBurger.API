using FluentValidation;
using OrderBurger.API.DTOs;
using OrderBurger.API.Repositories;

namespace OrderBurger.API.Validators;

public class OrderValidator : AbstractValidator<OrderRequestDTO>
{
    private readonly IProductRepository _productRepository;

    public OrderValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Pedido deve conter pelo menos um produto")
            .Must(HaveNoDuplicateProducts)
            .WithMessage("O pedido contém produtos duplicados")
            .Must(HaveNoDuplicateCategories)
            .WithMessage("O pedido contém produtos com categorias duplicadas");

        RuleForEach(x => x.Items)
            .SetValidator(new OrderItemValidator());
    }

    private static bool HaveNoDuplicateProducts(IEnumerable<OrderItemRequestDTO> items)
    {
        var duplicatedProducts = items
            .GroupBy(x => x.ProductId)
            .Any(g => g.Count() > 1);

        return !duplicatedProducts;
    }

    private bool HaveNoDuplicateCategories(
        IEnumerable<OrderItemRequestDTO> items)
    {
        var productIds = items.Select(i => i.ProductId).Distinct().ToList();

        var products = _productRepository.GetByIds(productIds);
        var categories = products.Select(p => p.Category).ToList();

        var duplicatedCategories = categories
            .GroupBy(c => c)
            .Any(g => g.Count() > 1);

        return !duplicatedCategories;
    }
}