using OrderBurger.API.DTOs;
using FluentValidation;

namespace OrderBurger.API.Validators;

public class ProductValidator: AbstractValidator<ProductRequestDTO>
{
    public ProductValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Código do produto obrigatório");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome do produto obrigatório!");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Descrição do produto obrigatória");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Preço do produto deve ser maior que zero!");
    }   
    
}