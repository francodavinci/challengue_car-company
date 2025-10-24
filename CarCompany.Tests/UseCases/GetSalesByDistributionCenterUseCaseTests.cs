using Moq;
using Xunit;
using CarCompany.Application.UseCases;
using CarCompany.Application.DTOs;
using CarCompany.Domain.Entities;
using CarCompany.Domain.Interfaces;
using CarCompany.Domain.Exceptions;
using CarCompany.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace CarCompany.Tests.UseCases
{
    public class GetSalesByDistributionCenterUseCaseTests
    {
        private readonly Mock<ISalesRepository> _mockSalesRepository;
        private readonly Mock<IDistributionCenterRepository> _mockDistributionCenterRepository;
        private readonly Mock<ILogger<GetSalesByDistributionCenterUseCase>> _mockLogger;
        private readonly GetSalesByDistributionCenterUseCase _useCase;

        public GetSalesByDistributionCenterUseCaseTests()
        {
            _mockSalesRepository = new Mock<ISalesRepository>();
            _mockDistributionCenterRepository = new Mock<IDistributionCenterRepository>();
            _mockLogger = new Mock<ILogger<GetSalesByDistributionCenterUseCase>>();
            _useCase = new GetSalesByDistributionCenterUseCase(
                _mockSalesRepository.Object,
                _mockDistributionCenterRepository.Object,
                _mockLogger.Object);
        }

        [Fact]
        public void Execute_WithValidDistributionCenter_ShouldReturnCorrectTotals()
        {
            // Arrange
            var distributionCenterId = Guid.NewGuid();
            var distributionCenter = new DistributionCenter(Guid.NewGuid(), "Test Center");
            
            var sales = new List<Sale>
            {
                new Sale(new Car(TypeCar.SEDAN), distributionCenterId),
                new Sale(new Car(TypeCar.SUV), distributionCenterId)
            };

            _mockDistributionCenterRepository
                .Setup(x => x.GetById(distributionCenterId))
                .Returns(distributionCenter);

            _mockSalesRepository
                .Setup(x => x.GetByDistributionCenter(distributionCenterId))
                .Returns(sales);

            // Act
            var result = _useCase.Execute(distributionCenterId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalUnits);
            Assert.Equal(sales.Sum(s => s.Car.Price), result.TotalAmount);

            _mockDistributionCenterRepository.Verify(x => x.GetById(distributionCenterId), Times.Once);
            _mockSalesRepository.Verify(x => x.GetByDistributionCenter(distributionCenterId), Times.Once);
        }

        [Fact]
        public void Execute_WithNonExistentDistributionCenter_ShouldThrowException()
        {
            // Arrange
            var distributionCenterId = Guid.NewGuid();

            _mockDistributionCenterRepository
                .Setup(x => x.GetById(distributionCenterId))
                .Returns((DistributionCenter?)null);

            // Act & Assert
            var exception = Assert.Throws<DistributionCenterNotFoundException>(() => _useCase.Execute(distributionCenterId));
            Assert.Contains(distributionCenterId.ToString(), exception.Message);

            _mockDistributionCenterRepository.Verify(x => x.GetById(distributionCenterId), Times.Once);
            _mockSalesRepository.Verify(x => x.GetByDistributionCenter(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void Execute_WithNoSales_ShouldReturnZeroTotals()
        {
            // Arrange
            var distributionCenterId = Guid.NewGuid();
            var distributionCenter = new DistributionCenter(Guid.NewGuid(), "Test Center");
            var sales = new List<Sale>();

            _mockDistributionCenterRepository
                .Setup(x => x.GetById(distributionCenterId))
                .Returns(distributionCenter);

            _mockSalesRepository
                .Setup(x => x.GetByDistributionCenter(distributionCenterId))
                .Returns(sales);

            // Act
            var result = _useCase.Execute(distributionCenterId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TotalUnits);
            Assert.Equal(0, result.TotalAmount);
        }

        [Fact]
        public void Execute_WithSingleSale_ShouldReturnCorrectValues()
        {
            // Arrange
            var distributionCenterId = Guid.NewGuid();
            var distributionCenter = new DistributionCenter(Guid.NewGuid(), "Test Center");
            var sale = new Sale(new Car(TypeCar.SEDAN), distributionCenterId);
            var sales = new List<Sale> { sale };

            _mockDistributionCenterRepository
                .Setup(x => x.GetById(distributionCenterId))
                .Returns(distributionCenter);

            _mockSalesRepository
                .Setup(x => x.GetByDistributionCenter(distributionCenterId))
                .Returns(sales);

            // Act
            var result = _useCase.Execute(distributionCenterId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TotalUnits);
            Assert.Equal(sale.Car.Price, result.TotalAmount);
        }
    }
}
