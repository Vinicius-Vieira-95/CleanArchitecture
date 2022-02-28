using CleanArch.Domain.Entities;
using FluentValidation;

namespace CleanArch.Domain.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        
        public ProductValidator()
        {
            RuleFor( p => p.Id)
                .LessThan(0).WithMessage("Id não pode ser negativo");

            RuleFor(p => p.Name)
                .MinimumLength(2)
                .MaximumLength(60)
                .NotNull().WithMessage("Nome não pode ser nulo")
                .Empty().WithMessage("Um nome tem que exitir");

            RuleFor(p => p.Price)
                .Must( price => price > 0).WithMessage("Preço não pode ser negativo");

        }
    }
}