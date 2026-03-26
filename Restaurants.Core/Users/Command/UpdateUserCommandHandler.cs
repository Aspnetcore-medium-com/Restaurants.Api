using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Core.User;
using Restaurants.Domain.Entities;
using Restaurants.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Exceptions;

namespace Restaurants.Core.Users.Command
{
    public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger,
            IUserStore<ApplicationUser> userStore, IUserContext userContext) : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            CurrentUser? user = userContext.GetCurrentUser();
            logger.LogInformation("updating user {UserId} with {@Request}", user.Id, request);
            if (user == null) { throw new NotFoundException("user not found in context"); }
            ApplicationUser? appUser = await userStore.FindByIdAsync(user.Id,cancellationToken);
            if (appUser == null)
            {
                throw new NotFoundException("user not found in store");
            }
            appUser.Nationality = request.Nationality;
            appUser.DateOfBirth = request.DateOnly;
            userStore.UpdateAsync(appUser, cancellationToken);
        }
    }
}
