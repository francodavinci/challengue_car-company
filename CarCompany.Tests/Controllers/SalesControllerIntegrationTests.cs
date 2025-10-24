using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarCompany.API.Controllers;
using CarCompany.Application.UseCases;
using CarCompany.Application.DTOs;
using CarCompany.Domain.Exceptions;
using CarCompany.Domain.Enums;
using CarCompany.Domain.Entities;
using CarCompany.Infrastructure.Repositories;
using CarCompany.Domain.Interfaces;
using Moq;

namespace CarCompany.Tests.Controllers
{
    public class SalesControllerIntegrationTests
    {
        private readonly SalesController _controller;
        private readonly ISalesRepository _salesRepository;
        private readonly IDistributionCenterRepository _distributionCenterRepository;

        public SalesControllerIntegrationTests()
        {
            _salesRepository = new SalesRepository();
            _distributionCenterRepository = new DistributionCenterRepository();
            
            var logger = new Mock<ILogger<SalesController>>();
            var createSaleUseCase = new CreateSaleUseCase(_salesRepository, _distributionCenterRepository, new Mock<ILogger<CreateSaleUseCase>>().Object);
            var getTotalSalesUseCase = new GetTotalSalesUseCase(_salesRepository, new Mock<ILogger<GetTotalSalesUseCase>>().Object);
            var getSalesByDistributionCenterUseCase = new GetSalesByDistributionCenterUseCase(_salesRepository, _distributionCenterRepository, new Mock<ILogger<GetSalesByDistributionCenterUseCase>>().Object);
            var getUnitsSalesPercentageByDistributionCenter = new GetUnitsSalesPercentageByDistributionCenter(_salesRepository, _distributionCenterRepository, new Mock<ILogger<GetUnitsSalesPercentageByDistributionCenter>>().Object);

            _controller = new SalesController(
                createSaleUseCase,
                getTotalSalesUseCase,
                getSalesByDistributionCenterUseCase,
                getUnitsSalesPercentageByDistributionCenter,
                logger.Object);
        }

        [Fact]
        public void Post_WithValidRequest_ShouldReturnCreatedResult()
        {
            // Arrange - Use an existing distribution center ID
            var distributionCenters = _distributionCenterRepository.GetAll();
            var firstCenter = distributionCenters.First();
            
            var request = new SaleRequest
            {
                DistributionCenterID = firstCenter.Id,
                CarType = TypeCar.SEDAN
            };

            // Act
            var result = _controller.Post(request);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.NotNull(createdResult.Value);
        }

        [Fact]
        public void GetTotalSales_ShouldReturnOkResult()
        {
            // Act
            var result = _controller.GetTotalSales();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetSalesByDistributionCenter_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var distributionCenters = _distributionCenterRepository.GetAll();
            var firstCenter = distributionCenters.First();

            // Act
            var result = _controller.GetSalesByDistributionCenter(firstCenter.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetSalesPercentageByCenter_ShouldReturnOkResult()
        {
            // Act
            var result = _controller.GetSalesPercentageByCenter();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}
