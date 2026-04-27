using AutoMapper;
using OrderBurger.API.DTOs;
using OrderBurger.API.Models;

namespace OrderBurger.API.Mappings;

public sealed class OrderProfile: Profile
{
    // public OrderProfile()
    // {
    //     CreateMap<OrderItem, OrderItemResponseDTO>()
    //         .ForCtorParam(nameof(OrderItemResponseDTO.ProductName), opt => opt.MapFrom(_ => string.Empty))
    //         .ForCtorParam(nameof(OrderItemResponseDTO.ProductCode), opt => opt.MapFrom(_ => string.Empty));
    //
    //     CreateMap<Order, OrderResponseDTO>();
    // }
    
    public OrderProfile()
    {
        CreateMap<OrderItem, OrderItemResponseDTO>()
            .ConstructUsing(src => new OrderItemResponseDTO(
                src.ProductId,
                src.Product != null ? src.Product.Name : string.Empty,
                src.Product != null ? src.Product.Code : string.Empty,
                src.Quantity,
                src.UnitPrice,
                src.Total
            ));

        CreateMap<Order, OrderResponseDTO>()
            .ConstructUsing((src, ctx) => new OrderResponseDTO(
                src.Id,
                src.ConsumerName,
                src.SubTotal,
                src.Discount,
                src.Total,
                src.Status,
                src.Items.Select(i => ctx.Mapper.Map<OrderItemResponseDTO>(i))
            ));
        
    }    
}