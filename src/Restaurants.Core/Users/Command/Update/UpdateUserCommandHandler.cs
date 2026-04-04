using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Exceptions;
using Restaurants.Core.Users.User;

namespace Restaurants.Core.Users.Command.Update
{
    public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger,
            IUserStore<ApplicationUser> userStore, IUserContext userContext) : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            CurrentUser? user = userContext.GetCurrentUser();
            logger.LogInformation("updating user {UserId} with {@Request}", user.Id, request);
            if (user == null) { throw new NotFoundException("user not found in context",user); }
            ApplicationUser? appUser = await userStore.FindByIdAsync(user.Id,cancellationToken);
            if (appUser == null)
            {
                throw new NotFoundException("user not found in store", user);
            }
            appUser.Nationality = request.Nationality;
            appUser.DateOfBirth = request.DateOfBirth;
            userStore.UpdateAsync(appUser, cancellationToken);
        }
    }
}
