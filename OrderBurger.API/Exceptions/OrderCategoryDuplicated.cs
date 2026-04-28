namespace OrderBurger.API.Exceptions;

public sealed class OrderCategoryDuplicated() : BaseException("Existe mais de um produto com a mesma categoria no pedido, favor verificar!.");