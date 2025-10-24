using Moq;
using Xunit;
using CarCompany.Application.UseCases;
using CarCompany.Application.DTOs;
using CarCompany.Domain.Entities;
using CarCompany.Domain.Interfaces;
using CarCompany.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace CarCompany.Tests.UseCases
{
    public class GetUnitsSalesPercentageByDistributionCenterTests
    {
        private readonly Mock<ISalesRepository> _mockSalesRepository;
        private readonly Mock<IDistributionCenterRepository> _mockDistributionCenterRepository;
        private readonly Mock<ILogger<GetUnitsSalesPercentageByDistributionCenter>> _mockLogger;
        private readonly GetUnitsSalesPercentageByDistributionCenter _useCase;

        public GetUnitsSalesPercentageByDistributionCenterTests()
        {
            _mockSalesRepository = new Mock<ISalesRepository>();
            _mockDistributionCenterRepository = new Mock<IDistributionCenterRepository>();
            _mockLogger = new Mock<ILogger<GetUnitsSalesPercentageByDistributionCenter>>();
            _useCase = new GetUnitsSalesPercentageByDistributionCenter(
                _mockSalesRepository.Object,
                _mockDistributionCenterRepository.Object,
                _mockLogger.Object);
        }

        [Fact]
        public void GetUnitsSalesPercentageByCenter_WithSales_ShouldReturnCorrectPercentages()
        {
            // Arrange
            var center1Id = Guid.NewGuid();
            var center2Id = Guid.NewGuid();
            
            var distributionCenters = new List<DistributionCenter>
            {
                new DistributionCenter(center1Id, "Center 1"),
                new DistributionCenter(center2Id, "Center 2")
            };

            var sales = new List<Sale>
            {
                new Sale(new Car(TypeCar.SEDAN), center1Id),
                new Sale(new Car(TypeCar.SEDAN), center1Id),
                new Sale(new Car(TypeCar.SUV), center2Id)
            };

            _mockDistributionCenterRepository
                .Setup(x => x.GetAll())
                .Returns(distributionCenters);

            _mockSalesRepository
                .Setup(x => x.GetAll())
                .Returns(sales);

            // Act
            var result = _useCase.GetUnitsSalesPercentageByCenter();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.CenterPercentages);
            Assert.Equal(2, result.CenterPercentages.Count);

            var center1Data = result.CenterPercentages.FirstOrDefault(c => c.CenterName == "Center 1");
            var center2Data = result.CenterPercentages.FirstOrDefault(c => c.CenterName == "Center 2");

            Assert.NotNull(center1Data);
            Assert.NotNull(center2Data);

            // Center 1 should have 2 SEDAN sales (66.67% of total)
            Assert.True(center1Data.ModelPercentages.ContainsKey(TypeCar.SEDAN));
            Assert.Equal(2, center1Data.ModelPercentages[TypeCar.SEDAN].Units);
            Assert.Equal(66.67m, center1Data.ModelPercentages[TypeCar.SEDAN].Percentage);

            // Center 2 should have 1 SUV sale (33.33% of total)
            Assert.True(center2Data.ModelPercentages.ContainsKey(TypeCar.SUV));
            Assert.Equal(1, center2Data.ModelPercentages[TypeCar.SUV].Units);
            Assert.Equal(33.33m, center2Data.ModelPercentages[TypeCar.SUV].Percentage);
        }

        [Fact]
        public void GetUnitsSalesPercentageByCenter_WithNoSales_ShouldReturnEmptyResult()
        {
            // Arrange
            var distributionCenters = new List<DistributionCenter>
            {
                new DistributionCenter(Guid.NewGuid(), "Center 1")
            };

            var sales = new List<Sale>();

            _mockDistributionCenterRepository
                .Setup(x => x.GetAll())
                .Returns(distributionCenters);

            _mockSalesRepository
                .Setup(x => x.GetAll())
                .Returns(sales);

            // Act
            var result = _useCase.GetUnitsSalesPercentageByCenter();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.CenterPercentages);
            Assert.Empty(result.CenterPercentages);
        }

        [Fact]
        public void GetUnitsSalesPercentageByCenter_WithNoDistributionCenters_ShouldReturnEmptyResult()
        {
            // Arrange
            var distributionCenters = new List<DistributionCenter>();
            var sales = new List<Sale>
            {
                new Sale(new Car(TypeCar.SEDAN), Guid.NewGuid())
            };

            _mockDistributionCenterRepository
                .Setup(x => x.GetAll())
                .Returns(distributionCenters);

            _mockSalesRepository
                .Setup(x => x.GetAll())
                .Returns(sales);

            // Act
            var result = _useCase.GetUnitsSalesPercentageByCenter();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.CenterPercentages);
            Assert.Empty(result.CenterPercentages);
        }

        [Fact]
        public void GetUnitsSalesPercentageByCenter_WithSingleSale_ShouldReturnCorrectPercentage()
        {
            // Arrange
            var centerId = Guid.NewGuid();
            var distributionCenters = new List<DistributionCenter>
            {
                new DistributionCenter(centerId, "Center 1")
            };

            var sales = new List<Sale>
            {
                new Sale(new Car(TypeCar.SEDAN), centerId)
            };

            _mockDistributionCenterRepository
                .Setup(x => x.GetAll())
                .Returns(distributionCenters);

            _mockSalesRepository
                .Setup(x => x.GetAll())
                .Returns(sales);

            // Act
            var result = _useCase.GetUnitsSalesPercentageByCenter();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.CenterPercentages);

            var centerData = result.CenterPercentages.First();
            Assert.Equal("Center 1", centerData.CenterName);
            Assert.True(centerData.ModelPercentages.ContainsKey(TypeCar.SEDAN));
            Assert.Equal(1, centerData.ModelPercentages[TypeCar.SEDAN].Units);
            Assert.Equal(100.00m, centerData.ModelPercentages[TypeCar.SEDAN].Percentage);
        }

        [Fact]
        public void GetUnitsSalesPercentageByCenter_WithMultipleCarTypes_ShouldCalculateCorrectPercentages()
        {
            // Arrange
            var centerId = Guid.NewGuid();
            var distributionCenters = new List<DistributionCenter>
            {
                new DistributionCenter(centerId, "Center 1")
            };

            var sales = new List<Sale>
            {
                new Sale(new Car(TypeCar.SEDAN), centerId),
                new Sale(new Car(TypeCar.SEDAN), centerId),
                new Sale(new Car(TypeCar.SUV), centerId),
                new Sale(new Car(TypeCar.SUV), centerId),
                new Sale(new Car(TypeCar.SUV), centerId)
            };

            _mockDistributionCenterRepository
                .Setup(x => x.GetAll())
                .Returns(distributionCenters);

            _mockSalesRepository
                .Setup(x => x.GetAll())
                .Returns(sales);

            // Act
            var result = _useCase.GetUnitsSalesPercentageByCenter();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.CenterPercentages);

            var centerData = result.CenterPercentages.First();
            Assert.Equal("Center 1", centerData.CenterName);
            
            // SEDAN: 2 units = 40% of total
            Assert.True(centerData.ModelPercentages.ContainsKey(TypeCar.SEDAN));
            Assert.Equal(2, centerData.ModelPercentages[TypeCar.SEDAN].Units);
            Assert.Equal(40.00m, centerData.ModelPercentages[TypeCar.SEDAN].Percentage);

            // SUV: 3 units = 60% of total
            Assert.True(centerData.ModelPercentages.ContainsKey(TypeCar.SUV));
            Assert.Equal(3, centerData.ModelPercentages[TypeCar.SUV].Units);
            Assert.Equal(60.00m, centerData.ModelPercentages[TypeCar.SUV].Percentage);
        }
    }
}
