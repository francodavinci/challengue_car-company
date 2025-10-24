using CarCompany.Domain.Exceptions;
using CarCompany.Domain.Enums;
using Xunit;

namespace CarCompany.Tests.Domain.Exceptions
{
    public class InvalidCarTypeExceptionTests
    {
        [Fact]
        public void Constructor_WithInvalidCarType_ShouldSetProperties()
        {
            // Arrange
            var invalidCarType = (TypeCar)999;

            // Act
            var exception = new InvalidCarTypeException(invalidCarType);

            // Assert
            Assert.Equal(invalidCarType, exception.CarType);
            Assert.Contains("Invalid car type", exception.Message);
            Assert.Contains("999", exception.Message);
            Assert.Contains("SEDAN, SUV, OFFROAD, SPORT", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldInheritFromDomainException()
        {
            // Arrange
            var invalidCarType = (TypeCar)999;

            // Act
            var exception = new InvalidCarTypeException(invalidCarType);

            // Assert
            Assert.IsAssignableFrom<DomainException>(exception);
        }
    }
}
