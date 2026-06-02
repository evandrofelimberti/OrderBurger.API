namespace OrderBurger.API.Enums;

public enum TransactionType
{
    None,
    Inbound,
    Outbound
}

public enum TransactionStatus
{
    None,
    Open,
    Closed,
    Cancelled
}

public enum BusinessPartnerType
{
    None,
    Customer,
    Supplier,
    Vendor
}

public enum DocumentType
{
    None,
    Cpf,
    Cnpj
}


