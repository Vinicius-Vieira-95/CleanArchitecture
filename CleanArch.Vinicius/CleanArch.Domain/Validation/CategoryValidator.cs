using CleanArch.Domain.Entities;
using FluentValidation;

namespace CleanArch.Domain.Validation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor( c => c.Id)
                .LessThan(0).WithMessage("Id não pode ser negativo");

            RuleFor(c => c.Name)
                .MinimumLength(3)
                .MaximumLength(60)
                .NotEmpty()
                .NotNull();
                
        }
    }
}