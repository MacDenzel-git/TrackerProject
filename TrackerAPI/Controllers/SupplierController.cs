using BusinessLogicLayer.Services.SupplierServiceContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;
        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating client Type
        /// </summary>
        /// <param name="Supplier"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(SupplierDTO supplier)
        {
            var outputHandler = await _service.Create(supplier);
            if (outputHandler.IsErrorOccured)
            {
                return BadRequest(outputHandler);
            }
            return Ok(outputHandler);
        }

        /// <summary>
        /// This is the API for updating client Type
        /// </summary>
        /// <param name="Supplier"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(SupplierDTO supplier)
        {
            var outputHandler = await _service.Update(supplier);
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

        [HttpGet("GetAllSuppliers")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var output = await _service.GetAllSuppliers();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        /// 
        

        [HttpPost("DeleteRequest")]
        public async Task<IActionResult> Delete(SupplierDTO supplier)
        {
            var output = await _service.DeleteRequest(supplier);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            return Ok(output);
        }




        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// 
        [HttpPost("DeleteApproval")]
        public async Task<IActionResult> DeleteApproval(SupplierDTO supplier)
        {
            var output = await _service.DeleteApprove(supplier);
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

        [HttpGet("GetSupplier")]
        public async Task<IActionResult> GetSupplier(int SupplierId)
        {
            var output = await _service.GetSupplier(SupplierId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
