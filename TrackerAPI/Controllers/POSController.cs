using AllinOne.DataHandlers;
using BusinessLogicLayer.Services.POSServiceContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class POSController : Controller
    {
        private readonly IPosService _posService;

        public POSController(IPosService posService)
        {
            _posService = posService;
        }

        [HttpPost("SearchProduct")]
        public async Task<IActionResult> SearchProduct(ProductSearchParamDTO productSearchParam)
        {
            var product = await _posService.GetProduct(productSearchParam);
            if (product == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Product Not Found"
                });
            }
            else
            {
                return Ok(product);
            }
        }
    }
}
