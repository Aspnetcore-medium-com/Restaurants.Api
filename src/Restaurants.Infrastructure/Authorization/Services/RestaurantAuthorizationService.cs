using Microsoft.Extensions.Logging;
using Restaurants.Core.Users.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Services
{
    

    public class RestaurantAuthorizationService(IUserContext userContext, ILogger<RestaurantAuthorizationService> logger) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            // Get the currently logged-in user from context (claims/principal)
            var user = userContext.GetCurrentUser();

            // Log who is trying to perform what operation on which resource
            logger.LogInformation(
                "Auth invoked for {Resource} to do {Operation} by {User}",
                restaurant, resourceOperation, user?.Email);

            // If no user is found in context → this is an invalid state → throw exception
            if (user == null)
            {
                throw new NotFoundException("user", nameof(user));
            }

            // READ and CREATE operations are allowed for any authenticated user
            if (resourceOperation == ResourceOperation.Read
                || resourceOperation == ResourceOperation.Create)
            {
                // Log successful authorization for read/create
                logger.LogInformation("Create / Read operation - successful authorization");

                return true; // allow access
            }

            // DELETE is allowed if the user is in Admin role
            else if (resourceOperation == ResourceOperation.Delete
                     && user.IsInRole(UserRoles.Admin))
            {
                // Log admin privilege usage
                logger.LogInformation("user is an admin - Delete allowed");

                return true; // allow access
            }

            // UPDATE or DELETE is allowed if the user is the owner of the restaurant
            else if ((resourceOperation == ResourceOperation.Update
                      || resourceOperation == ResourceOperation.Delete)
                     && restaurant.OwnerId == Guid.Parse(user.Id))
            {
                // Log owner-based authorization
                logger.LogInformation("user is a owner - Update / delete allowed");

                return true; // allow access
            }

            // If none of the above conditions match → deny access
            return false;
        }
    }
}
