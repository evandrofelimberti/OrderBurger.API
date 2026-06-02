using AutoMapper;
using OrderBurger.API.DTOs;
using OrderBurger.API.Models;

namespace OrderBurger.API.Mappings;

public sealed class InventoryTransactionProfile : Profile
{
    public InventoryTransactionProfile()
    {
        CreateMap<InventoryTransactionItem, InventoryTransactionItemResponseDTO>()
            .ForCtorParam("ProductName", o => o.MapFrom(s => s.Product != null ? s.Product.Name : string.Empty))
            .ForCtorParam("ProductCode", o => o.MapFrom(s => s.Product != null ? s.Product.Code : string.Empty));

        CreateMap<InventoryTransaction, InventoryTransactionResponseDTO>()
            .ForCtorParam("BusinessPartnerName", o => o.MapFrom(s => s.BusinessPartner != null ? s.BusinessPartner.Name : null));
    }
}
