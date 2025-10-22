using Microsoft.AspNetCore.Mvc;
using CarCompany.Application.DTOs;
using CarCompany.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(CreateSaleRequest saleAddRequest)
        {
            try
            {
                var saleResponse = await _salesService.CreateSaleAsync(saleAddRequest);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("invalid parameter");
                    return BadRequest(ModelState);
                }

                return CreatedAtAction(nameof(SaleResponse), saleResponse);
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
