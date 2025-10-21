using Microsoft.AspNetCore.Mvc;
using CarCompany.Application.DTOs;
using CarCompany.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CarCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : Controller { 


        //private fields 
        private readonly ISalesService _salesService;
        
        //constructor
        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(CreateSaleRequest saleAddRequest)
        {
            var response = new ApiResponse { Messages = { "asd"} };
            return Ok(response);
        }
    }
}
