using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorization
{
    public class RestaurantsUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public RestaurantsUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            // Add Nationality claim
            if (!string.IsNullOrEmpty(user.Nationality))
            {
                identity.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
            }

            // Add DateOfBirth claim
            if (user.DateOfBirth.HasValue)
            {
                identity.AddClaim(new Claim(
                   AppClaimTypes.DateOfBirth,
                    user.DateOfBirth.Value.ToString("yyyy-MM-dd")
                ));
            }

            return identity;
        }
    }
}