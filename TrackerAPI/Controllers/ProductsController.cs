using BusinessLogicLayer.Services.OrderDetailServiceContainer;
using BusinessLogicLayer.Services.OrderServiceContainer;
using BusinessLogicLayer.Services.ProductServiceContainer;
using BusinessLogicLayer.Services.ProductsServiceContainer;
using BusinessLogicLayer.Services.SupplierServiceContainer;
using BusinessLogicLayer.Services.TransactionTypeServiceContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating client Type
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductDTO category)
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
        /// <param name="Product"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(ProductDTO product)
        {
            var outputHandler = await _service.Update(product);
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

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var output = await _service.GetAllProducts();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(ProductDTO product)
        {
            var output = await _service.DeleteRequest(product);
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

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int ProductId)
        {
            var output = await _service.GetProduct(ProductId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
