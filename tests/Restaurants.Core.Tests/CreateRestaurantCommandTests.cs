using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Create;
using Restaurants.Core.Users.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Exceptions;
using Restaurants.Infrastructure.Authorization.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Tests
{
    public class CreateRestaurantCommandTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly IFixture _fixture;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
        private readonly Mock<ILogger<CreateRestaurantsCommandHandler>> _loggerMock;
        private readonly Mock<IUserContext> _userContextMock;
        private readonly Mock<IRestaurantAuthorizationService> _restAuthMock;
        private readonly CreateRestaurantsCommandHandler _sut;

        public CreateRestaurantCommandTests() {
            //Automatically create mocks for interfaces.
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _mapperMock = _fixture.Freeze<Mock<IMapper>>();
            _restaurantsRepositoryMock = _fixture.Freeze<Mock<IRestaurantsRepository>>();
            _userContextMock = _fixture.Freeze<Mock<IUserContext>>();
            _restAuthMock = _fixture.Freeze<Mock<IRestaurantAuthorizationService>>();
            _loggerMock = _fixture.Freeze<Mock<ILogger<CreateRestaurantsCommandHandler>>>();
            _fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _sut = _fixture.Create<CreateRestaurantsCommandHandler>();
            _fixture.Register(() => DateOnly.FromDateTime(DateTime.Today));

        }

        [Fact]
        public async Task Handler_WhenUserNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = _fixture.Create<CreateRestaurantsCommand>();
            _userContextMock.Setup(u => u.GetCurrentUser()).Returns((CurrentUser?)null);

            // Act
            Func<Task> act = async () => await _sut.Handle(request, CancellationToken.None);
            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handler_WhenAuthorizationFailed_ShouldReturnForbiddenException()
        {
            // Arrange
            var request = _fixture.Create<CreateRestaurantsCommand>();
            var mockUser = _fixture.Build<CurrentUser>().With(u => u.Id, Guid.NewGuid().ToString())
                .Create(); 
            _userContextMock.Setup(u => u.GetCurrentUser()).Returns(mockUser);
            var restaurant = _fixture.Create<Restaurant>();
            restaurant.OwnerId = Guid.Empty;
            _mapperMock.Setup(m => m.Map<Restaurant>(request)).Returns(restaurant);
            _restAuthMock.Setup(r => r.Authorize(restaurant, ResourceOperation.Create)).Returns(false);
            // Act
            Func<Task> act = async () => await _sut.Handle(request, CancellationToken.None);
            // Assert
            await act.Should().ThrowAsync<ForbiddenException>();
        }

        [Fact]
        public async Task Handler_WhenValidRequest_ShouldReturnRestaurantId()
        {
            // Arrange
            var request = _fixture.Create<CreateRestaurantsCommand>();
            var mockUser = _fixture.Build<CurrentUser>().With(u => u.Id, Guid.NewGuid().ToString())
                .Create();
            _userContextMock.Setup(u => u.GetCurrentUser()).Returns(mockUser);
            var restaurant = _fixture.Create<Restaurant>();
            restaurant.OwnerId = Guid.Empty;
            _mapperMock.Setup(m => m.Map<Restaurant>(request)).Returns(restaurant);
            _restAuthMock.Setup(r => r.Authorize(restaurant, ResourceOperation.Create)).Returns(true);
            _restaurantsRepositoryMock.Setup(r => r.CreateRestaurantAsync(It.IsAny<Restaurant>()
                    ,It.IsAny<CancellationToken>())).ReturnsAsync(1);
            // Act
            var result = await _sut.Handle(request,CancellationToken.None);
            // Assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be(restaurant.OwnerId);
            _restaurantsRepositoryMock.Verify(r => r.CreateRestaurantAsync(restaurant,It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
