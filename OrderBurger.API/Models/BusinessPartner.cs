using OrderBurger.API.DTOs;
using OrderBurger.API.Enums;

namespace OrderBurger.API.Models;

public class BusinessPartner
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public DocumentType DocumentType { get; set; } = DocumentType.None;
    public BusinessPartnerType Type { get; set; } = BusinessPartnerType.None;

    public BusinessPartner() { }

    public BusinessPartner(string name, string documentNumber, DocumentType documentType, BusinessPartnerType type)
    {
        Id = Guid.NewGuid();
        Name = name;
        DocumentNumber = documentNumber;
        DocumentType = documentType;
        Type = type;
    }

    public void UpdateFromRequest(BusinessPartnerRequestDTO dto)
    {
        Name = dto.Name;
        DocumentNumber = dto.DocumentNumber;
        DocumentType = dto.DocumentType;
        Type = dto.Type;
    }
}
