using Microsoft.AspNetCore.Mvc;
using CarCompany.Application.DTOs;
using CarCompany.Application.UseCases;
using CarCompany.Domain.Exceptions;

namespace CarCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : Controller
    {
        private readonly CreateSaleUseCase _createSaleUseCase;
        private readonly GetTotalSalesUseCase _getTotalSalesUseCase;
        private readonly GetSalesByDistributionCenterUseCase _getSalesByDistributionCenterUseCase;
        private readonly GetUnitsSalesPercentageByDistributionCenter _getUnitsSalesPercentageByDistributionCenter;
        private readonly ILogger<SalesController> _logger;

        public SalesController(
            CreateSaleUseCase createSaleUseCase,
            GetTotalSalesUseCase getTotalSalesUseCase,
            GetSalesByDistributionCenterUseCase getSalesByDistributionCenterUseCase,
            GetUnitsSalesPercentageByDistributionCenter getUnitsSalesPercentageByDistributionCenter,
            ILogger<SalesController> logger)
        {
            _createSaleUseCase = createSaleUseCase;
            _getTotalSalesUseCase = getTotalSalesUseCase;
            _getSalesByDistributionCenterUseCase = getSalesByDistributionCenterUseCase;
            _getUnitsSalesPercentageByDistributionCenter = getUnitsSalesPercentageByDistributionCenter;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(SaleRequest saleRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid parameters");
                    return BadRequest(ModelState);
                }

                var result = _createSaleUseCase.Execute(saleRequest);
                return Created("", result);
            }
            catch (DistributionCenterNotFoundException ex)
            {
                _logger.LogWarning(ex, "Distribution center not found");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(TotalSalesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTotalSales()
        {
            try
            {
                var result = _getTotalSalesUseCase.Execute();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total sales");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("{distributionCenterId}")]
        [ProducesResponseType(typeof(SalesByDistributionCenterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSalesByDistributionCenter(Guid distributionCenterId)
        {
            try
            {
                var result = _getSalesByDistributionCenterUseCase.Execute(distributionCenterId);
                return Ok(result);
            }
            catch (DistributionCenterNotFoundException ex)
            {
                _logger.LogWarning(ex, "Distribution center not found");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sales by distribution center");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("percentage-by-center")]
        [ProducesResponseType(typeof(SalesUnitsPercentageByCenterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSalesPercentageByCenter()
        {
            try
            {
                var result = _getUnitsSalesPercentageByDistributionCenter.GetUnitsSalesPercentageByCenter();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sales percentage by center");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}