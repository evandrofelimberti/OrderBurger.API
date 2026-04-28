namespace OrderBurger.API.Exceptions;

public sealed class OrderProductDuplicate() : BaseException("Existe produto duplicado no pedido");