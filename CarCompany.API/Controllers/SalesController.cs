using Microsoft.AspNetCore.Mvc;
using CarCompany.Application.DTOs;
using CarCompany.Application.UseCases;
using CarCompany.Domain.Exceptions;

namespace CarCompany.API.Controllers
{
    /// <summary>
    /// Controller for automobile sales management
    /// </summary>
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

        /// <summary>
        /// Creates a new automobile sale
        /// </summary>
        /// <param name="saleRequest">Sale data to create</param>
        /// <returns>Information of the created sale</returns>
        /// <response code="201">Sale created successfully</response>
        /// <response code="400">Invalid input data or car type</response>
        /// <response code="404">Distribution center not found</response>
        /// <response code="500">Internal server error</response>
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
            catch (InvalidCarTypeException ex)
            {
                _logger.LogWarning(ex, "Invalid car type provided");
                return BadRequest(new { message = ex.Message, carType = ex.CarType });
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

        /// <summary>
        /// Gets the total sales and units sold
        /// </summary>
        /// <returns>Total sales summary</returns>
        /// <response code="200">Data retrieved successfully</response>
        /// <response code="500">Internal server error</response>
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

        /// <summary>
        /// Gets sales by specific distribution center
        /// </summary>
        /// <param name="distributionCenterId">Distribution center ID</param>
        /// <returns>Sales for the specified center</returns>
        /// <response code="200">Data retrieved successfully</response>
        /// <response code="404">Distribution center not found</response>
        /// <response code="500">Internal server error</response>
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

        /// <summary>
        /// Gets the percentage of units sold by car model in each center
        /// </summary>
        /// <returns>Sales percentages by center and model</returns>
        /// <response code="200">Data retrieved successfully</response>
        /// <response code="500">Internal server error</response>
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