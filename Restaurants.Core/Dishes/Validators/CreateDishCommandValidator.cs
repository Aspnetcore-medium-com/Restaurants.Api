using FluentValidation;
using Restaurants.Core.Dishes.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dishes.Validators
{
    public class CreateDishCommandValidator: AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator() {
            RuleFor(d => d.KiloCalories).GreaterThanOrEqualTo(0).WithMessage("Kilo calories must be greater than 0");
            RuleFor(d => d.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be greater than 0");
        }
    }
}
