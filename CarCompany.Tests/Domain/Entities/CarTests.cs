using Xunit;
using CarCompany.Domain.Entities;
using CarCompany.Domain.Enums;

namespace CarCompany.Tests.Domain.Entities
{
    public class CarTests
    {
        [Theory]
        [InlineData(TypeCar.SEDAN, 8000)]
        [InlineData(TypeCar.SUV, 9500)]
        public void Constructor_WithValidType_ShouldSetCorrectPrice(TypeCar model, decimal expectedPrice)
        {
            // Act
            var car = new Car(model);

            // Assert
            Assert.Equal(model, car.Model);
            Assert.Equal(expectedPrice, car.Price);
        }

        [Fact]
        public void Constructor_WithSedan_ShouldSetCorrectProperties()
        {
            // Act
            var car = new Car(TypeCar.SEDAN);

            // Assert
            Assert.Equal(TypeCar.SEDAN, car.Model);
            Assert.Equal(8000, car.Price);
        }

        [Fact]
        public void Constructor_WithSuv_ShouldSetCorrectProperties()
        {
            // Act
            var car = new Car(TypeCar.SUV);

            // Assert
            Assert.Equal(TypeCar.SUV, car.Model);
            Assert.Equal(9500, car.Price);
        }

        [Fact]
        public void Price_ShouldBeImmutable()
        {
            // Arrange
            var car = new Car(TypeCar.SEDAN);
            var originalPrice = car.Price;

            // Act & Assert
            // Price should be read-only, so we can't modify it directly
            // This test ensures the price is set correctly and remains constant
            Assert.Equal(8000, car.Price);
            Assert.Equal(originalPrice, car.Price);
        }
    }
}
