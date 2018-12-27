using EE.Hillewaere.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Hillewaere.Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .Length(3, 20)
                .WithMessage("Length must be between 3 and 20 characters");

            RuleFor(product => product.Price)
                .NotEmpty()
                .WithMessage("Price cannot be empty")
                .GreaterThan(0)
                .WithMessage("Must be greater than 0");

            RuleFor(product => product.Code)
                .NotEmpty()
                .WithMessage("Code cannot be empty")
                .MaximumLength(20)
                .WithMessage("Length cannot be greater than 20 characters");

            RuleFor(product => product.Description)
                .NotEqual(p => p.Name)
                .WithMessage("Must be different than Name");
        }
    }
}
