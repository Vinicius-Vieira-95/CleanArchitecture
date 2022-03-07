using CleanArch.Domain.Entities;
using FluentValidation;

namespace CleanArch.Domain.Validation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {

            RuleFor(c => c.Name)
                .MinimumLength(3).WithMessage("Nome pode ter no minimo 3 caracteres")
                .MaximumLength(60).WithMessage("Nome pode conter no maximo 60 caracteres")
                .NotEmpty()
                .NotNull();
        }
    }
}