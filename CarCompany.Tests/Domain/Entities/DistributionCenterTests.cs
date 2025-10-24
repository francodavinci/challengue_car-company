using Xunit;
using CarCompany.Domain.Entities;

namespace CarCompany.Tests.Domain.Entities
{
    public class DistributionCenterTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldSetProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Test Center";

            // Act
            var center = new DistributionCenter(id, name);

            // Assert
            Assert.Equal(id, center.Id);
            Assert.Equal(name, center.Name);
        }

        [Fact]
        public void Constructor_WithDifferentNames_ShouldCreateDifferentCenters()
        {
            // Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var name1 = "Center 1";
            var name2 = "Center 2";

            // Act
            var center1 = new DistributionCenter(id1, name1);
            var center2 = new DistributionCenter(id2, name2);

            // Assert
            Assert.NotEqual(center1.Id, center2.Id);
            Assert.Equal(name1, center1.Name);
            Assert.Equal(name2, center2.Name);
        }

        [Fact]
        public void Constructor_WithSameId_ShouldCreateSameId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name1 = "Center 1";
            var name2 = "Center 2";

            // Act
            var center1 = new DistributionCenter(id, name1);
            var center2 = new DistributionCenter(id, name2);

            // Assert
            Assert.Equal(center1.Id, center2.Id);
            Assert.Equal(name1, center1.Name);
            Assert.Equal(name2, center2.Name);
        }

        [Theory]
        [InlineData("Center A")]
        [InlineData("Center B")]
        [InlineData("")]
        public void Constructor_WithVariousNames_ShouldSetCorrectValues(string name)
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var center = new DistributionCenter(id, name);

            // Assert
            Assert.Equal(id, center.Id);
            Assert.Equal(name, center.Name);
        }
    }
}