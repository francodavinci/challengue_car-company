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
    public class CreateSaleUseCaseTests
    {
        private readonly Mock<ISalesRepository> _mockSalesRepository;
        private readonly Mock<IDistributionCenterRepository> _mockDistributionCenterRepository;
        private readonly Mock<ILogger<CreateSaleUseCase>> _mockLogger;
        private readonly CreateSaleUseCase _useCase;

        public CreateSaleUseCaseTests()
        {
            _mockSalesRepository = new Mock<ISalesRepository>();
            _mockDistributionCenterRepository = new Mock<IDistributionCenterRepository>();
            _mockLogger = new Mock<ILogger<CreateSaleUseCase>>();
            _useCase = new CreateSaleUseCase(
                _mockSalesRepository.Object,
                _mockDistributionCenterRepository.Object,
                _mockLogger.Object);
        }

        [Fact]
        public void Execute_WithValidRequest_ShouldReturnSaleResponse()
        {
            // Arrange
            var distributionCenterId = Guid.NewGuid();
            var request = new SaleRequest
            {
                DistributionCenterID = distributionCenterId,
                CarType = TypeCar.SEDAN
            };

            var distributionCenter = new DistributionCenter(Guid.NewGuid(), "Test Center");
            var expectedSale = new Sale(new Car(TypeCar.SEDAN), distributionCenterId);

            _mockDistributionCenterRepository
                .Setup(x => x.GetById(distributionCenterId))
                .Returns(distributionCenter);

            _mockSalesRepository
                .Setup(x => x.Add(It.IsAny<Sale>()))
                .Returns(expectedSale);

            // Act
            var result = _useCase.Execute(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedSale.Id, result.ID);
            Assert.Equal(TypeCar.SEDAN, result.Car.Model);
            Assert.Equal(distributionCenterId, result.DistributionCenterID);

            _mockDistributionCenterRepository.Verify(x => x.GetById(distributionCenterId), Times.Once);
            _mockSalesRepository.Verify(x => x.Add(It.IsAny<Sale>()), Times.Once);
        }

        [Fact]
        public void Execute_WithNonExistentDistributionCenter_ShouldThrowException()
        {
            // Arrange
            var distributionCenterId = Guid.NewGuid();
            var request = new SaleRequest
            {
                DistributionCenterID = distributionCenterId,
                CarType = TypeCar.SEDAN
            };

            _mockDistributionCenterRepository
                .Setup(x => x.GetById(distributionCenterId))
                .Returns((DistributionCenter?)null);

            // Act & Assert
            var exception = Assert.Throws<DistributionCenterNotFoundException>(() => _useCase.Execute(request));
            Assert.Contains(distributionCenterId.ToString(), exception.Message);

            _mockDistributionCenterRepository.Verify(x => x.GetById(distributionCenterId), Times.Once);
            _mockSalesRepository.Verify(x => x.Add(It.IsAny<Sale>()), Times.Never);
        }

        [Theory]
        [InlineData(TypeCar.SEDAN)]
        [InlineData(TypeCar.SUV)]
        public void Execute_WithDifferentCarTypes_ShouldCreateCorrectCar(TypeCar carType)
        {
            // Arrange
            var distributionCenterId = Guid.NewGuid();
            var request = new SaleRequest
            {
                DistributionCenterID = distributionCenterId,
                CarType = carType
            };

            var distributionCenter = new DistributionCenter(Guid.NewGuid(), "Test Center");
            var expectedSale = new Sale(new Car(carType), distributionCenterId);

            _mockDistributionCenterRepository
                .Setup(x => x.GetById(distributionCenterId))
                .Returns(distributionCenter);

            _mockSalesRepository
                .Setup(x => x.Add(It.IsAny<Sale>()))
                .Returns(expectedSale);

            // Act
            var result = _useCase.Execute(request);

            // Assert
            Assert.Equal(carType, result.Car.Model);
        }
    }
}
