using FluentAssertions;
using OrderBurger.API.Business.OrderDiscount;
using OrderBurger.API.Enums;
using OrderBurger.API.Models;

namespace OrderBurger.API.Tests.Business.OrderDiscount;

public class ComboOrderDiscountStrategyTests
{
    private readonly ComboOrderDiscountStrategy _orderDiscount = new();

    [Fact]
    public void CalculateDiscount_Combo_Complete_20_Porcent()
    {
        // Arrange
        var order = BuildOrder(
            CreateProduct("SAND-1", "Sanduíche", 30m, CategoryEnum.Sandwich),
            CreateProduct("SODA-1", "Refrigerante", 10m, CategoryEnum.Soda),
            CreateProduct("FRIES-1", "Batata", 10m, CategoryEnum.Fries));

        // Act
        var discount = _orderDiscount.CalculateDiscount(order);

        // Assert
        discount.Should().Be(10.00m);
    }

    [Fact]
    public void CalculateDiscount_15_Porcent()
    {
        var order = BuildOrder(
            CreateProduct("SAND-1", "Sanduíche", 40m, CategoryEnum.Sandwich),
            CreateProduct("SODA-1", "Refrigerante", 20m, CategoryEnum.Soda));

        var discount = _orderDiscount.CalculateDiscount(order);

        discount.Should().Be(9.00m);
    }

    [Fact]
    public void CalculateDiscount_10_Porcent()
    {
        var order = BuildOrder(
            CreateProduct("SAND-1", "Sanduíche", 30m, CategoryEnum.Sandwich),
            CreateProduct("FRIES-1", "Batata", 20m, CategoryEnum.Fries));

        var discount = _orderDiscount.CalculateDiscount(order);

        discount.Should().Be(5.00m);
    }

    [Fact]
    public void CalculateDiscount_NoCombo_Zero_Porcent()
    {
        var order = BuildOrder(
            CreateProduct("SODA-1", "Refrigerante", 10m, CategoryEnum.Soda),
            CreateProduct("SODA-2", "Refrigerante Zero", 15m, CategoryEnum.Soda));

        var discount = _orderDiscount.CalculateDiscount(order);

        discount.Should().Be(0m);
    }
    

    private static Order BuildOrder(params Product[] products)
    {
        var order = new Order("Cliente Teste");
        foreach (var product in products)
            order.AddItem(product, 1);

        return order;
    }

    private static Product CreateProduct(string code, string name, decimal price, CategoryEnum category)
        => new(code, name, "Desc", price, category);
}
