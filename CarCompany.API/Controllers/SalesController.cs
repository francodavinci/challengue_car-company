using Microsoft.AspNetCore.Mvc;
using CarCompany.Application.DTOs;
using CarCompany.Application.UseCases;
using CarCompany.Domain.Exceptions;
using System.Diagnostics;

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
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogInformation("Starting POST /api/sales endpoint execution");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid parameters");
                    return BadRequest(ModelState);
                }

                var result = _createSaleUseCase.Execute(saleRequest);
                
                stopwatch.Stop();
                _logger.LogInformation("POST /api/sales endpoint completed successfully in {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                
                return Created("", result);
            }
            catch (InvalidCarTypeException ex)
            {
                stopwatch.Stop();
                _logger.LogWarning(ex, "Invalid car type provided. Endpoint failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                return BadRequest(new { message = ex.Message, carType = ex.CarType });
            }
            catch (DistributionCenterNotFoundException ex)
            {
                stopwatch.Stop();
                _logger.LogWarning(ex, "Distribution center not found. Endpoint failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Error creating sale. Endpoint failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(TotalSalesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTotalSales()
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogInformation("Starting GET /api/sales endpoint execution");

                var result = _getTotalSalesUseCase.Execute();
                
                stopwatch.Stop();
                _logger.LogInformation("GET /api/sales endpoint completed successfully in {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Error getting total sales. Endpoint failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
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
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogInformation("Starting GET /api/sales/{{distributionCenterId}} endpoint execution for ID: {DistributionCenterId}", distributionCenterId);

                var result = _getSalesByDistributionCenterUseCase.Execute(distributionCenterId);
                
                stopwatch.Stop();
                _logger.LogInformation("GET /api/sales/{{distributionCenterId}} endpoint completed successfully in {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                
                return Ok(result);
            }
            catch (DistributionCenterNotFoundException ex)
            {
                stopwatch.Stop();
                _logger.LogWarning(ex, "Distribution center not found. Endpoint failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Error getting sales by distribution center. Endpoint failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("percentage-by-center")]
        [ProducesResponseType(typeof(SalesUnitsPercentageByCenterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSalesPercentageByCenter()
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                _logger.LogInformation("Starting GET /api/sales/percentage-by-center endpoint execution");

                var result = _getUnitsSalesPercentageByDistributionCenter.GetUnitsSalesPercentageByCenter();
                
                stopwatch.Stop();
                _logger.LogInformation("GET /api/sales/percentage-by-center endpoint completed successfully in {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Error getting sales percentage by center. Endpoint failed after {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}