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
        [HttpGet("GetLowStockProduct")]
        public async Task<IActionResult> GetLowStockProduct(int shopId)
        {
            var products = await _posService.GetLowStockProduct(shopId);
            if (products == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Product Not Found"
                });
            }
            else
            {
                return Ok(products);
            }
        }
       
        
        [HttpGet("GetExpiredInventory")]
        public async Task<IActionResult> GetExpiredInventory(int shopId)
        {
            var expiredInventory = await _posService.GetExpiredInventory(shopId);
            if (expiredInventory == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Product Not Found"
                });
            }
            else
            {
                return Ok(expiredInventory);
            }
        }

         [HttpGet("GetCartItems")]
        public async Task<IActionResult> GetCartItems(string receipt)
        {
            var cartItems = await _posService.GetCartItems(receipt);
            if (cartItems == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Cart items Not Found"
                });
            }
            else
            {
                return Ok(cartItems);
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


        [HttpGet("GetShopTransactions")]
        public async Task<IActionResult> GetShopTransactions(int shopId)
        {
            var transactions = await _posService.GetAllTransactions(shopId);
            if (transactions == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Transactions Not Found"
                });
            }
            else
            {
                return Ok(transactions);
            }
        } 
        
        
        [HttpGet("Dashboard")]
        public async Task<IActionResult> DasboardConfigure(int shopId)
        {
            var dashboardITems = await _posService.Dashboard(shopId);
            if (dashboardITems == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Dashboard Configuration Failed"
                });
            }
            else
            {
                return Ok(dashboardITems);
            }
        }
        
        
        [HttpPut("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart(CartItemsDTO cartItem)
        {
            var dashboardITems = await _posService.RemoveFromCart(cartItem);
            if (dashboardITems == null)
            {
                return Ok(new OutputHandler
                {
                    Message = "Cart Item Removed"
                });
            }
            else
            {
                return Ok(dashboardITems);
            }
        }

    }
}
