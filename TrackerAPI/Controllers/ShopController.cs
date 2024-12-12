using BusinessLogicLayer.Services.ShopServiceContainer;
using BusinessLogicLayer.Services.SupplierServiceContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _service;
        public ShopController(IShopService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating client Type
        /// </summary>
        /// <param name="Shop"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ShopDTO shop)
        {
            var outputHandler = await _service.Create(shop);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating client Type
        /// </summary>
        /// <param name="Shop"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(ShopDTO shop)
        {
            var outputHandler = await _service.Update(shop);
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

        [HttpGet("GetAllShops")]
        public async Task<IActionResult> GetAllShops()
        {
            var output = await _service.GetAllShops();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="ShopId"></param>
        /// <returns></returns>
        /// 
        [HttpPost("DeleteRequest")]
        public async Task<IActionResult> Delete(ShopDTO shopDTO)
        {
            var output = await _service.DeleteRequest(shopDTO);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }




        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="ShopId"></param>
        /// <returns></returns>
        /// 
        [HttpPost("DeleteApproval")]
        public async Task<IActionResult> DeleteApproval(ShopDTO shopDTO)
        {
            var output = await _service.DeleteApprove(shopDTO);
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

        [HttpGet("GetShop")]
        public async Task<IActionResult> GetShop(int ShopId)
        {
            var output = await _service.GetShop(ShopId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
