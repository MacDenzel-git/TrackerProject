using BusinessLogicLayer.Services.CategoryServiceContainer;
using BusinessLogicLayer.Services.SupplierServiceContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace TrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is the API for creating client Type
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CategoryDTO category)
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
        /// <param name="Category"></param>
        /// <returns></returns>
        /// 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(CategoryDTO category)
        {
            var outputHandler = await _service.Update(category);
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

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var output = await _service.GetAllCategories();
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();
        }

        /// <summary>
        /// This is the API that deletes a client Type
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        /// 
        [HttpPost("DeleteRequest")]
        public async Task<IActionResult> Delete(CategoryDTO categoryDTO)
        {
            var output = await _service.DeleteRequest(categoryDTO);
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
        public async Task<IActionResult> DeleteApproval(CategoryDTO categoryDTO)
        {
            var output = await _service.DeleteApprove(categoryDTO);
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

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(int CategoryId)
        {
            var output = await _service.GetCategory(CategoryId);
            if (output != null)
            {
                return Ok(output);
            }
            return NoContent();

        }

    }
}
