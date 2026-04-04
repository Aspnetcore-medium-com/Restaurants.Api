using FluentAssertions;
using System.Net;
namespace Restaurants.Api.IntegrationTests
{
    public class RestaurantsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public RestaurantsControllerTests(CustomWebApplicationFactory factory) { 
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_WhenValidQuery_ShouldReturn200Ok()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("/api/restaurants?pageNumber=1&pageSize=10");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_WhenInValidQuery_ShouldReturn404Ok()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("/api/restaurants?pageNumber=-1&pageSize=10");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetById_WhenIdExists_ShouldReturn200()
        {
            var response = await _httpClient.GetAsync("/api/restaurants/1");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_WhenIdDoesntExist_ShouldReturn404()
        {
            var response = await _httpClient.GetAsync("/api/restaurants/10");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}