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
                .WithMessage("Price cannot be empty");

            //RuleFor(product => product.)
            //    .NotEmpty()
            //    .WithMessage("")


        }
    }
}
