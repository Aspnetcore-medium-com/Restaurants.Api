using FluentValidation;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Validators
{
    public class UpdateRestaurantRequestValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantRequestValidator() {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name should not be empty")
                .Length(3, 50).WithMessage("Name should be greater than 3 and less than 100");
            RuleFor(r => r.Description).NotEmpty().WithMessage("Desc should not be empty")
                .Length(3, 100).WithMessage("should be less than 100");
            RuleFor(r => r.HasDelivery).NotEmpty().WithMessage("Select delivery preference");
        }
    }
}
