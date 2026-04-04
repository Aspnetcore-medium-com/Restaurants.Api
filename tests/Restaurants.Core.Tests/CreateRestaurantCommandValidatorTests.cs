


using FluentAssertions;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Create;
using Restaurants.Core.Validators;

namespace Restaurants.Core.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact]
        public void Validator_WhenValidData_ShouldPass()
        {
            // Arrange
            var createCommandValidator = new CreateRestaurantCommandValidator();
            var createCommand = new CreateRestaurantsCommand()
            {
                Name = "test name",
                ContactEmail = "test@test.com",
                Category = "Indian",
                Description = "This is the Description",
                Street = "street"
            };
            // Act
            var result = createCommandValidator.Validate(createCommand);

            // Assert
            result.IsValid.Should().BeTrue();   
        }

        [Fact]
        public void Validator_WhenValidData_ShouldFail()
        {
            // Arrange
            var createCommandValidator = new CreateRestaurantCommandValidator();
            var createCommand = new CreateRestaurantsCommand()
            {
                Name = "t",
                ContactEmail = "test",
                Category = "BIndian",
                Description = "",
                Street = ""
            };
            // Act
            var result = createCommandValidator.Validate(createCommand);

            // Assert
            result.IsValid.Should().BeFalse();
        }


        [Theory]
        [InlineData("Indian")]
        [InlineData("Chinese")]
        [InlineData("Italian")]
        [InlineData("English")]
        [InlineData("Turkish")]
        public void Validator_WhenValidCategory_ShouldPass(string Category)
        {
            var createCommandValidator = new CreateRestaurantCommandValidator();
            var createCommand = new CreateRestaurantsCommand()
            {
                Name = "test name",
                ContactEmail = "test@test.com",
                Category = Category,
                Description = "This is the Description",
                Street = "street"
            };
            // Act
            var result = createCommandValidator.Validate(createCommand);
            // Assert
            result.IsValid.Should().BeTrue();
        }

    }
}
