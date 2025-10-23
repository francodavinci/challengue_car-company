using Microsoft.AspNetCore.Mvc;
using CarCompany.Application.DTOs;
using CarCompany.Application.Interfaces;

namespace CarCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : Controller
    {
        //private fields 
        private readonly ISalesService _salesService;
        private readonly ILogger<SalesController> _logger;

        //constructor
        public SalesController(ISalesService salesService, ILogger<SalesController> logger)
        {
            _salesService = salesService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  IActionResult Post(SaleRequest saleAddRequest)
        {
            try
            {
                var saleResponse =  _salesService.CreateSale(saleAddRequest);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("invalid parameter");
                    return BadRequest(ModelState);
                }

                return Created("sale successfully created", saleResponse);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "validation error");
                return BadRequest(new { message = ex.Message});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "internal server error Creating a new sale");
                return StatusCode(500, new { message = "internal server error" });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(TotalSalesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTotalSalesVolume()
        {
            try
            {
                var result = _salesService.GetTotalSalesVolume();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total sales volume");
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
                var result = _salesService.GetSalesByDistributionCenter(distributionCenterId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "validation error");
                return BadRequest(new { message = ex.Message });
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
                var result = _salesService.GetUnitsSalesPercentageByCenter();
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
