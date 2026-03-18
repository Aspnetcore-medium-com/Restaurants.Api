using FluentValidation;
using Restaurants.Core.Dtos.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Validators
{
    public class CreateRestaurantDtoValidator : AbstractValidator<RestaurantRequestDto>
    {
        private readonly List<string> types = ["Indian", "Chinese", "Italian", "English", "Turkish"];
        public CreateRestaurantDtoValidator() {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(r => r.ContactEmail).EmailAddress().WithMessage("Invalid Email");
            RuleFor(r => r.Category).Must(types.Contains).WithMessage("Invalid Category");
            RuleFor(r => r.Description).Length(3, 200).WithMessage("Description should be within the length");
            RuleFor(r => r.Street).NotEmpty().WithMessage("Street should not be empty");


        }
    }
}
