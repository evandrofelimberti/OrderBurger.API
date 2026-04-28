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
            return Math.Round(order.SubTotal * ComboOrderDiscount.ComboSandwichSodaFries, 2, MidpointRounding.AwayFromZero);
        if (existSandwich && existSoda)
            return Math.Round(order.SubTotal *  ComboOrderDiscount.ComboSandwichSoda, 2, MidpointRounding.AwayFromZero);
        if (existSandwich && existFries)
            return Math.Round(order.SubTotal *  ComboOrderDiscount.ComboSandwichFries, 2, MidpointRounding.AwayFromZero);       
        
        return ComboOrderDiscount.ComboNone;
    }
    
}