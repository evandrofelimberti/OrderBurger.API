using OrderBurger.API.Enums;
using OrderBurger.API.Models;

namespace OrderBurger.API.Business.OrderDiscount;

public class ComboOrderDiscountStrategy: IOrderDiscountStrategy
{
    public decimal CalculateDiscount(Order order)
    {
        var existSandwich = order.Items.Any(i => i.Category == CategoryEnum.Sandwich);
        
        var existSoda = order.Items.Any(i => i.Category == CategoryEnum.Soda);
        
        var existFries = order.Items.Any(i => i.Category == CategoryEnum.Fries);
        
        if (existSandwich && existSoda && existFries)
            return Math.Round(order.SubTotal * 0.20m, 2, MidpointRounding.AwayFromZero);
        if (existSandwich && existSoda)
            return Math.Round(order.SubTotal *  0.15m, 2, MidpointRounding.AwayFromZero);
        if (existSandwich && existFries)
            return Math.Round(order.SubTotal *  0.10m, 2, MidpointRounding.AwayFromZero);       
        
        return 0;
    }
    
}