using BusinessLogicLayer.Services.OrderServiceContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating client Type
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(OrderDTO order)
        {
            var outputHandler = await _service.Create(order);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating client Type
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(OrderDTO order)
        {
            var outputHandler = await _service.Update(order);
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

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var output = await _service.GetAllOrders();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(OrderDTO order)
        {
            var output = await _service.DeleteRequest(order);
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

        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder(int OrderId)
        {
            var output = await _service.GetOrder(OrderId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
