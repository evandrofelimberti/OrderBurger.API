using AutoMapper;
using OrderBurger.API.DTOs;
using OrderBurger.API.Models;

namespace OrderBurger.API.Mappings;

public sealed class BusinessPartnerProfile : Profile
{
    public BusinessPartnerProfile()
    {
        CreateMap<BusinessPartner, BusinessPartnerResponseDTO>();
    }
}
