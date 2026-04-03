

using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Core.Users.User;
using System.Security.Claims;
using System.Security.Principal;

namespace Restaurants.Core.Tests
{
    public class UserContextTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IHttpContextAccessor> _contextAccessorMock;
        private readonly UserContext _sut;

        public UserContextTests() {
            //Automatically create mocks for interfaces.
            _fixture = new Fixture().Customize(new AutoMoqCustomization()); 
            // Usercontext constructor requires IHttpContextAccessor
            _contextAccessorMock = _fixture.Freeze< Mock<IHttpContextAccessor>>();
            _sut = _fixture.Create<UserContext>();
           
        }

        [Fact]
        public void GetCurrentUser_WhenIsAuthenticatedIsFalse_ShouldReturnNull()
        {
            //Arrange
            // Passing no authenticationType means IsAuthenticated = false
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext();
            httpContext.User = user;

            _contextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
            //act
            var result = _sut.GetCurrentUser();
            //assert
            result.Should().BeNull();
        } 

        [Fact]
        public void GetCurrentUser_WhenIdentityIsNull_ShouldReturnNull()
        {
            // Arrange
            var user = new Mock<ClaimsPrincipal>();
            user.Setup(x => x.Identity).Returns((IIdentity?)null);
            var httpContext = new DefaultHttpContext();
            httpContext.User = user.Object;
            // Act
            var result = _sut.GetCurrentUser();
            // Assert
            result?.Should().BeNull();
        }

        [Fact]
        public void GetCurrentUser_WhenValidClaims_ShouldReturnCurrentUser()
        {
            //Arrange
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, "Test"),
                new Claim(ClaimTypes.Email,"test@test.com"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("Nationality","UK"),
                new Claim("DateOfBirth","2000-10-10")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext();
            httpContext.User = user;
            _contextAccessorMock.Setup(c => c.HttpContext).Returns(httpContext);

            //Act
            var result = _sut.GetCurrentUser();
            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be("Test");
            result.Email.Should().Be("test@test.com");
            result.Roles.Should().Contain("Admin");
        }
    }
}
