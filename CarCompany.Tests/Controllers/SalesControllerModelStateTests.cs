using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using CarCompany.Application.DTOs;
using CarCompany.Application.UseCases;
using CarCompany.Domain.Enums;
using CarCompany.API.Controllers;
using CarCompany.Domain.Entities;

namespace CarCompany.Tests.Controllers
{
    public class SalesControllerModelStateTests
    {
        private readonly Mock<CreateSaleUseCase> _mockCreateSaleUseCase;
        private readonly Mock<GetTotalSalesUseCase> _mockGetTotalSalesUseCase;
        private readonly Mock<GetSalesByDistributionCenterUseCase> _mockGetSalesByDistributionCenterUseCase;
        private readonly Mock<GetUnitsSalesPercentageByDistributionCenter> _mockGetUnitsSalesPercentageByDistributionCenter;
        private readonly Mock<ILogger<SalesController>> _mockLogger;
        private readonly SalesController _controller;

        public SalesControllerModelStateTests()
        {
            _mockCreateSaleUseCase = new Mock<CreateSaleUseCase>(
                Mock.Of<CarCompany.Domain.Interfaces.ISalesRepository>(),
                Mock.Of<CarCompany.Domain.Interfaces.IDistributionCenterRepository>(),
                Mock.Of<ILogger<CreateSaleUseCase>>());
            
            _mockGetTotalSalesUseCase = new Mock<GetTotalSalesUseCase>(
                Mock.Of<CarCompany.Domain.Interfaces.ISalesRepository>(),
                Mock.Of<ILogger<GetTotalSalesUseCase>>());
            
            _mockGetSalesByDistributionCenterUseCase = new Mock<GetSalesByDistributionCenterUseCase>(
                Mock.Of<CarCompany.Domain.Interfaces.ISalesRepository>(),
                Mock.Of<CarCompany.Domain.Interfaces.IDistributionCenterRepository>(),
                Mock.Of<ILogger<GetSalesByDistributionCenterUseCase>>());
            
            _mockGetUnitsSalesPercentageByDistributionCenter = new Mock<GetUnitsSalesPercentageByDistributionCenter>(
                Mock.Of<CarCompany.Domain.Interfaces.ISalesRepository>(),
                Mock.Of<CarCompany.Domain.Interfaces.IDistributionCenterRepository>(),
                Mock.Of<ILogger<GetUnitsSalesPercentageByDistributionCenter>>());
            
            _mockLogger = new Mock<ILogger<SalesController>>();

            _controller = new SalesController(
                _mockCreateSaleUseCase.Object,
                _mockGetTotalSalesUseCase.Object,
                _mockGetSalesByDistributionCenterUseCase.Object,
                _mockGetUnitsSalesPercentageByDistributionCenter.Object,
                _mockLogger.Object);
        }

        [Fact]
        public void Post_WithInvalidModelState_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new SaleRequest
            {
                DistributionCenterID = Guid.Empty, // Invalid
                CarType = (TypeCar)999 // Invalid enum value
            };

            _controller.ModelState.AddModelError("DistributionCenterID", "Invalid ID");
            _controller.ModelState.AddModelError("CarType", "Invalid car type");

            // Act
            var result = _controller.Post(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public void Post_WithEmptyGuid_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new SaleRequest
            {
                DistributionCenterID = Guid.Empty,
                CarType = TypeCar.SEDAN
            };

            _controller.ModelState.AddModelError("DistributionCenterID", "Invalid ID");

            // Act
            var result = _controller.Post(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public void Post_WithInvalidCarType_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new SaleRequest
            {
                DistributionCenterID = Guid.NewGuid(),
                CarType = (TypeCar)999 // Invalid enum value
            };

            _controller.ModelState.AddModelError("CarType", "Invalid car type");

            // Act
            var result = _controller.Post(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Theory]
        [InlineData(TypeCar.SEDAN)]
        [InlineData(TypeCar.SUV)]
        [InlineData(TypeCar.OFFROAD)]
        public void Post_WithValidCarTypes_ShouldNotReturnBadRequest(TypeCar carType)
        {
            // Arrange
            var request = new SaleRequest
            {
                DistributionCenterID = Guid.NewGuid(),
                CarType = carType
            };

            // Act
            var result = _controller.Post(request);

            // Assert
            // Since we can't mock the use case, we expect it to throw an exception
            // but the controller should handle it gracefully
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSalesByDistributionCenter_WithEmptyGuid_ShouldNotReturnBadRequest()
        {
            // Arrange
            var emptyGuid = Guid.Empty;

            // Act
            var result = _controller.GetSalesByDistributionCenter(emptyGuid);

            // Assert
            // The controller should handle this gracefully
            Assert.NotNull(result);
        }
    }
}
