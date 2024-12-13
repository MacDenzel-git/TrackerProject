using BusinessLogicLayer.Services.InventoryTransactionsServiceContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IInventoryTransactionService _service;
        public InventoryTransactionController(IInventoryTransactionService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating client Type
        /// </summary>
        /// <param name="InventoryTransaction"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(InventoryTransactionDTO inventoryTransaction)
        {
            var outputHandler = await _service.InBound(inventoryTransaction);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }


        /// <summary>
        /// This is the API for creating client Type
        /// </summary>
        /// <param name="transferDTO"></param>
        /// <returns></returns>
        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer(InventoryTransferDTO transferDTO)
        {
            var outputHandler = await _service.Transfer(transferDTO);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }
        /// <summary>
        /// This is the API for updating client Type
        /// </summary>
        /// <param name="InventoryTransaction"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(InventoryTransactionDTO inventoryTransaction)
        {
            var outputHandler = await _service.Update(inventoryTransaction);
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

        [HttpGet("GetAllInventoryTransactions")]
        public async Task<IActionResult> GetAllInventoryTransactions()
        {
            var output = await _service.GetAllInventoryTransactions();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="InventoryTransactionId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(InventoryTransactionDTO inventoryTransaction)
        {
            var output = await _service.DeleteRequest(inventoryTransaction);
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

        [HttpGet("GetInventoryTransaction")]
        public async Task<IActionResult> GetInventoryTransaction(int InventoryTransactionId)
        {
            var output = await _service.GetInventoryTransaction(InventoryTransactionId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
