using Xunit;
using CarCompany.Domain.Exceptions;

namespace CarCompany.Tests.Domain.Exceptions
{
    public class DomainExceptionTests
    {
        [Fact]
        public void DistributionCenterNotFoundException_WithId_ShouldSetCorrectMessage()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var exception = new DistributionCenterNotFoundException(id);

            // Assert
            Assert.NotNull(exception);
            Assert.Contains(id.ToString(), exception.Message);
            Assert.Contains("Distribution center", exception.Message);
            Assert.Contains("not found", exception.Message);
        }

        [Fact]
        public void DistributionCenterNotFoundException_WithEmptyGuid_ShouldSetCorrectMessage()
        {
            // Arrange
            var id = Guid.Empty;

            // Act
            var exception = new DistributionCenterNotFoundException(id);

            // Assert
            Assert.NotNull(exception);
            Assert.Contains(id.ToString(), exception.Message);
        }

        [Fact]
        public void DistributionCenterNotFoundException_ShouldInheritFromDomainException()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var exception = new DistributionCenterNotFoundException(id);

            // Assert
            Assert.IsAssignableFrom<DomainException>(exception);
        }

        [Fact]
        public void DistributionCenterNotFoundException_ShouldInheritFromException()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var exception = new DistributionCenterNotFoundException(id);

            // Assert
            Assert.IsAssignableFrom<Exception>(exception);
        }
    }
}
