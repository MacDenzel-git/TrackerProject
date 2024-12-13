using BusinessLogicLayer.Services.OrderDetailServiceContainer;
using BusinessLogicLayer.Services.OrderServiceContainer;
using BusinessLogicLayer.Services.ShopProductServiceContainer;
 using BusinessLogicLayer.Services.SupplierServiceContainer;
 using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopProductController : ControllerBase
    {
        private readonly IShopProductService _service;
        public ShopProductController(IShopProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating client Type
        /// </summary>
        /// <param name="ShopProduct"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ShopProductDTO category)
        {
            var outputHandler = await _service.Create(category);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating client Type
        /// </summary>
        /// <param name="ShopProduct"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(ShopProductDTO shopProduct)
        {
            var outputHandler = await _service.Update(shopProduct);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API that gets client Type 
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet("GetAllShopProducts")]
        public async Task<IActionResult> GetAllShopProducts(int shopId)
        {
            var output = await _service.GetAllShopProducts(shopId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="ShopProductId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(ShopProductDTO shopProduct)
        {
            var output = await _service.DeleteRequest(shopProduct);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }



        /// <summary>
        /// This is API that gets a client Type
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns></returns>
        /// 

        [HttpGet("GetShopProduct")]
        public async Task<IActionResult> GetShopProduct(int ShopProductId)
        {
            var output = await _service.GetShopProduct(ShopProductId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
