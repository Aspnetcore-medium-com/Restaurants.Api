using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Users.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.AgeRequirement
{
    internal class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
         IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("user : {Email} date of birth {Dob}", currentUser?.Email, currentUser?.DateofBirth);
            if (currentUser?.DateofBirth == null)
            {
                logger.LogWarning("user date of birth is null");
                context.Fail();
                return Task.CompletedTask;  
            }
            if (currentUser?.DateofBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today)) {
                logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            }
            

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
