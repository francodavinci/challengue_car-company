using CarCompany.Application.DTOs;
using CarCompany.Application.UseCases;
using CarCompany.Domain.Entities;
using CarCompany.Domain.Enums;
using CarCompany.Domain.Exceptions;
using CarCompany.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CarCompany.Tests.UseCases
{
    public class CreateSaleUseCaseTests
    {
        private readonly Mock<ISalesRepository> _mockSalesRepository;
        private readonly Mock<IDistributionCenterRepository> _mockDistributionCenterRepository;
        private readonly CreateSaleUseCase _useCase;

        public CreateSaleUseCaseTests()
        {
            _mockSalesRepository = new Mock<ISalesRepository>();
            _mockDistributionCenterRepository = new Mock<IDistributionCenterRepository>();
            _useCase = new CreateSaleUseCase(
                _mockSalesRepository.Object,
                _mockDistributionCenterRepository.Object);
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

            var distributionCenter = new DistributionCenter(distributionCenterId, "Test Center");
            var sale = new Sale(new Car(TypeCar.SEDAN), distributionCenterId);

            _mockDistributionCenterRepository.Setup(x => x.GetById(distributionCenterId))
                .Returns(distributionCenter);
            _mockSalesRepository.Setup(x => x.Add(It.IsAny<Sale>()))
                .Returns(sale);

            // Act
            var result = _useCase.Execute(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sale.Id, result.ID);
            Assert.Equal(sale.Car.Model, result.Car.Model);
            Assert.Equal(sale.DistributionCenterID, result.DistributionCenterID);
        }

        [Fact]
        public void Execute_WithInvalidCarType_ShouldThrowInvalidCarTypeException()
        {
            // Arrange
            var request = new SaleRequest
            {
                DistributionCenterID = Guid.NewGuid(),
                CarType = (TypeCar)999 // Invalid enum value
            };

            // Act & Assert
            var exception = Assert.Throws<InvalidCarTypeException>(() => _useCase.Execute(request));
            Assert.Equal((TypeCar)999, exception.CarType);
            Assert.Contains("Invalid car type", exception.Message);
        }

        [Fact]
        public void Execute_WithNonExistentDistributionCenter_ShouldThrowDistributionCenterNotFoundException()
        {
            // Arrange
            var distributionCenterId = Guid.NewGuid();
            var request = new SaleRequest
            {
                DistributionCenterID = distributionCenterId,
                CarType = TypeCar.SEDAN
            };

            _mockDistributionCenterRepository.Setup(x => x.GetById(distributionCenterId))
                .Returns((DistributionCenter)null);

            // Act & Assert
            var exception = Assert.Throws<DistributionCenterNotFoundException>(() => _useCase.Execute(request));
            Assert.Contains(distributionCenterId.ToString(), exception.Message);
        }
    }
}
