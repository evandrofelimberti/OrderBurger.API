namespace OrderBurger.API.Exceptions;

public sealed class ProductNotFoundException : BaseException
{
    public Guid ProductId { get;}
    
    public ProductNotFoundException(Guid id) : base($"Produto Id '{id} não encontrado.")
    {
        ProductId = id;
    }   
}