using Microsoft.AspNetCore.Mvc;
using CarCompany.Application.DTOs;
using CarCompany.Application.Services;

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
        public  IActionResult Post(CreateSaleRequest saleAddRequest)
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
    }
}
