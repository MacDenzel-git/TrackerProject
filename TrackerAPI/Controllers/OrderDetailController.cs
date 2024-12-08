using BusinessLogicLayer.Services.OrderDetailServiceContainer;
using BusinessLogicLayer.Services.SupplierServiceContainer;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _service;
        public OrderDetailController(IOrderDetailService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating client Type
        /// </summary>
        /// <param name="OrderDetail"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(OrderDetailDTO category)
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
        /// <param name="OrderDetail"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(OrderDetailDTO orderDetail)
        {
            var outputHandler = await _service.Update(orderDetail);
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

        [HttpGet("GetAllOrderDetails")]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var output = await _service.GetAllOrderDetails();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="OrderDetailId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(OrderDetailDTO orderDetail)
        {
            var output = await _service.DeleteRequest(orderDetail);
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

        [HttpGet("GetOrderDetail")]
        public async Task<IActionResult> GetOrderDetail(int OrderDetailId)
        {
            var output = await _service.GetOrderDetail(OrderDetailId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
