using FluentValidation;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Create;
using Restaurants.Core.Dtos.Restaurants.Queries.GetAllRestaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Validators
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private readonly int[] allowedPageSizes = [5, 10, 15, 20];
        private readonly string[] allowedKeys = [nameof(CreateRestaurantsCommand.Name),
            nameof(CreateRestaurantsCommand.Description),
            nameof(CreateRestaurantsCommand.Category)];
        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than 1");
            RuleFor(r => r.PageSize).Must(r => allowedPageSizes.Contains(r))
                .WithMessage($"Page size should be in {string.Join("," , allowedPageSizes )}");
            RuleFor(r => r.SortKey)
                .Must(value => allowedKeys.Contains(value))
                .When(r => r.SortKey != null)
                .WithMessage($"allowed sort keys {string.Join(",", allowedKeys)}");
        }
    }
}
