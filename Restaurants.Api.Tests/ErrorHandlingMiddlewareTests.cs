using AutoFixture;
using AutoFixture.AutoMoq;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Api.Middlewares;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Create;
using Restaurants.Domain.Entities;
using Restaurants.Exceptions;
using System.Threading.Tasks;

namespace Restaurants.Api.Tests
{
    public class ErrorHandlingMiddlewareTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<ILogger<ErrorHandlingMiddleware>> _loggerMock;
        private readonly Mock<RequestDelegate> _requestDelegateMock;
        private readonly ErrorHandlingMiddleware _sut;

        public ErrorHandlingMiddlewareTests()
        {
            //Automatically create mocks for interfaces.
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _loggerMock = _fixture.Freeze<Mock<ILogger<ErrorHandlingMiddleware>>>();
            _requestDelegateMock = _fixture.Freeze<Mock<RequestDelegate>>();
            //_sut = _fixture.Create<ErrorHandlingMiddleware>();
            _sut = new ErrorHandlingMiddleware(_requestDelegateMock.Object, _loggerMock.Object);
        }
        [Fact]
        public async Task Invoke_WhenNoException_shouldInvokeNext()
        {
            // Arrange
            _requestDelegateMock.Setup(r => r(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);
            var httpContext = new DefaultHttpContext();
      
            // Act
            await _sut.Invoke(httpContext);
            // Assert
            _requestDelegateMock.Verify(x => x(It.IsAny<HttpContext>()),Times.Once);
        }

        [Fact]
        public async Task Invoke_WhenNotFound_ShouldReturn404()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            _requestDelegateMock.Setup(r => r(It.IsAny<HttpContext>())).ThrowsAsync(new NotFoundException(nameof(Restaurant),"restaurant"));
            // Act
            await _sut.Invoke(httpContext);
            // Assert
            Assert.Equal(StatusCodes.Status404NotFound,httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenForbidden_ShouldReturn403()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            _requestDelegateMock.Setup(r => r(It.IsAny<HttpContext>())).ThrowsAsync(new ForbiddenException());
            // Act
            await _sut.Invoke(httpContext);
            // Assert
            Assert.Equal(StatusCodes.Status403Forbidden, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenException_ShouldReturn500()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            _requestDelegateMock.Setup(r => r(It.IsAny<HttpContext>())).ThrowsAsync(new Exception());
            // Act
            await _sut.Invoke(httpContext);
            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, httpContext.Response.StatusCode);
        }
    }
}