using Xunit;
using CarCompany.Domain.Entities;
using CarCompany.Domain.Enums;

namespace CarCompany.Tests.Domain.Entities
{
    public class SaleTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldSetProperties()
        {
            // Arrange
            var car = new Car(TypeCar.SEDAN);
            var distributionCenterId = Guid.NewGuid();

            // Act
            var sale = new Sale(car, distributionCenterId);

            // Assert
            Assert.NotEqual(Guid.Empty, sale.Id);
            Assert.Equal(car, sale.Car);
            Assert.Equal(distributionCenterId, sale.DistributionCenterID);
        }

        [Fact]
        public void Constructor_WithDifferentCarTypes_ShouldSetCorrectCar()
        {
            // Arrange
            var sedan = new Car(TypeCar.SEDAN);
            var suv = new Car(TypeCar.SUV);
            var distributionCenterId = Guid.NewGuid();

            // Act
            var sedanSale = new Sale(sedan, distributionCenterId);
            var suvSale = new Sale(suv, distributionCenterId);

            // Assert
            Assert.Equal(sedan, sedanSale.Car);
            Assert.Equal(suv, suvSale.Car);
            Assert.Equal(TypeCar.SEDAN, sedanSale.Car.Model);
            Assert.Equal(TypeCar.SUV, suvSale.Car.Model);
        }

        [Fact]
        public void Constructor_WithSameCar_ShouldCreateDifferentIds()
        {
            // Arrange
            var car = new Car(TypeCar.SEDAN);
            var distributionCenterId = Guid.NewGuid();

            // Act
            var sale1 = new Sale(car, distributionCenterId);
            var sale2 = new Sale(car, distributionCenterId);

            // Assert
            Assert.NotEqual(sale1.Id, sale2.Id);
            Assert.Equal(sale1.Car, sale2.Car);
            Assert.Equal(sale1.DistributionCenterID, sale2.DistributionCenterID);
        }

        [Fact]
        public void Constructor_WithDifferentDistributionCenters_ShouldSetCorrectIds()
        {
            // Arrange
            var car = new Car(TypeCar.SEDAN);
            var center1Id = Guid.NewGuid();
            var center2Id = Guid.NewGuid();

            // Act
            var sale1 = new Sale(car, center1Id);
            var sale2 = new Sale(car, center2Id);

            // Assert
            Assert.Equal(center1Id, sale1.DistributionCenterID);
            Assert.Equal(center2Id, sale2.DistributionCenterID);
            Assert.NotEqual(sale1.DistributionCenterID, sale2.DistributionCenterID);
        }

        [Fact]
        public void Id_ShouldBeUniqueForEachSale()
        {
            // Arrange
            var car = new Car(TypeCar.SEDAN);
            var distributionCenterId = Guid.NewGuid();
            var sales = new List<Sale>();

            // Act
            for (int i = 0; i < 10; i++)
            {
                sales.Add(new Sale(car, distributionCenterId));
            }

            // Assert
            var ids = sales.Select(s => s.Id).ToList();
            Assert.Equal(ids.Count, ids.Distinct().Count());
        }
    }
}
