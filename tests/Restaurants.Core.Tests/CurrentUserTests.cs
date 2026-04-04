using AutoFixture;
using FluentAssertions;
using Restaurants.Core.Users.User;
using Restaurants.Domain.Constants;

namespace Restaurants.Core.Tests
{
    public class CurrentUserTests
    {
        private readonly IFixture _fixture;

        public CurrentUserTests() { 
            _fixture = new Fixture();
            // for date time
            _fixture.Register(() => DateOnly.FromDateTime(DateTime.Today));
            
        }

        [Theory]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.Owner)]
        public void IsInRole_WhenAdminUser_ShouldReturnUser(string userRole)
        {
            //arrange
            var currentUser = _fixture.Build<CurrentUser>().With(r => r.Roles, new List<string> { UserRoles.Admin, UserRoles.Owner }).Create();
            //act
            var user = currentUser.IsInRole(userRole);
            //assert
            user.Should().BeTrue();
        }

        [Fact]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            //arrange
            var currentUser = _fixture.Build<CurrentUser>().With(r => r.Roles, new List<string> { UserRoles.Admin}).Create();
            //act
            var user = currentUser.IsInRole(UserRoles.Owner);
            //assert
            user.Should().BeFalse();
        }

        [Fact]
        public void IsInRole_WithLowerCaseMatchingRole_ShouldReturnFalse()
        {
            //arrange
            var currentUser = _fixture.Build<CurrentUser>().With(r => r.Roles, new List<string> { UserRoles.Admin, UserRoles.Owner }).Create();
            //act
            var user = currentUser.IsInRole(UserRoles.Owner.ToLower());
            //assert
            user.Should().BeFalse();
        }
    }
}