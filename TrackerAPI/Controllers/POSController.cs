using AllinOne.DataHandlers;
using BusinessLogicLayer.Services.POSServiceContainer;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
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

        [HttpGet("GetReceiptDetails")]
        public async Task<IActionResult> GetReceiptDetails(string receipt)
        {
            var product = await _posService.GetReceiptDetails(receipt);
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


        [HttpPost("SearchProduct")]
        public async Task<IActionResult> SearchProduct(ProductSearchDTO productSearchParam)
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

        
        [HttpPost("NewTransaction")]
        public async Task<IActionResult> NewTransaction(JournalEntryDTO jentryDTO)
        {
            try
            {
                var product = await _posService.NewTransaction(jentryDTO);
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
            catch (Exception ex)
            {

                return BadRequest(new OutputHandler { IsErrorOccured =  true, Message = ex.Message});
            }
        }
        
        [HttpPut("Payment")]
        public async Task<IActionResult> Payment(JournalEntryDTO jentryDTO)
        {
            try
            {
                var output = await _posService.Payment(jentryDTO);
                if (output == null)
                {
                    return Ok(output);
                }
                else
                {
                    return Ok(output);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new OutputHandler { IsErrorOccured = true, Message = ex.Message });
                
            }
        }
    }
}
