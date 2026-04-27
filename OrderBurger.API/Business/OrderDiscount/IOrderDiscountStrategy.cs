using OrderBurger.API.Models;

namespace OrderBurger.API.Business.OrderDiscount;

public interface IOrderDiscountStrategy
{
    public decimal CalculateDiscount(Order order);
}