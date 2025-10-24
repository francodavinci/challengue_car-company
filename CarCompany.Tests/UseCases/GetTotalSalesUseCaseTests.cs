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
    public class GetTotalSalesUseCaseTests
    {
        private readonly Mock<ISalesRepository> _mockSalesRepository;
        private readonly Mock<ILogger<GetTotalSalesUseCase>> _mockLogger;
        private readonly GetTotalSalesUseCase _useCase;

        public GetTotalSalesUseCaseTests()
        {
            _mockSalesRepository = new Mock<ISalesRepository>();
            _mockLogger = new Mock<ILogger<GetTotalSalesUseCase>>();
            _useCase = new GetTotalSalesUseCase(_mockSalesRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public void Execute_WithSales_ShouldReturnCorrectTotals()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale(new Car(TypeCar.SEDAN), Guid.NewGuid()),
                new Sale(new Car(TypeCar.SUV), Guid.NewGuid()),
                new Sale(new Car(TypeCar.SEDAN), Guid.NewGuid())
            };

            _mockSalesRepository
                .Setup(x => x.GetAll())
                .Returns(sales);

            // Act
            var result = _useCase.Execute();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.TotalUnits);
            Assert.Equal(sales.Sum(s => s.Car.Price), result.TotalSales);

            _mockSalesRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void Execute_WithNoSales_ShouldReturnZeroTotals()
        {
            // Arrange
            var sales = new List<Sale>();
            _mockSalesRepository
                .Setup(x => x.GetAll())
                .Returns(sales);

            // Act
            var result = _useCase.Execute();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TotalUnits);
            Assert.Equal(0, result.TotalSales);
        }

        [Fact]
        public void Execute_WithSingleSale_ShouldReturnCorrectValues()
        {
            // Arrange
            var sale = new Sale(new Car(TypeCar.SEDAN), Guid.NewGuid());
            var sales = new List<Sale> { sale };

            _mockSalesRepository
                .Setup(x => x.GetAll())
                .Returns(sales);

            // Act
            var result = _useCase.Execute();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TotalUnits);
            Assert.Equal(sale.Car.Price, result.TotalSales);
        }

        [Fact]
        public void Execute_WithMixedCarTypes_ShouldCalculateCorrectTotal()
        {
            // Arrange
            var sedanPrice = new Car(TypeCar.SEDAN).Price;
            var suvPrice = new Car(TypeCar.SUV).Price;
            var expectedTotal = sedanPrice + suvPrice;

            var sales = new List<Sale>
            {
                new Sale(new Car(TypeCar.SEDAN), Guid.NewGuid()),
                new Sale(new Car(TypeCar.SUV), Guid.NewGuid())
            };

            _mockSalesRepository
                .Setup(x => x.GetAll())
                .Returns(sales);

            // Act
            var result = _useCase.Execute();

            // Assert
            Assert.Equal(2, result.TotalUnits);
            Assert.Equal(expectedTotal, result.TotalSales);
        }
    }
}
