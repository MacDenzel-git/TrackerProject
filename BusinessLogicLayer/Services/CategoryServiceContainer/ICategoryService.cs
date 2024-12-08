using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.CategoryServiceContainer
{
    public interface ICategoryService
    {
        Task<OutputHandler> Create(CategoryDTO categoryDTO);
        Task<OutputHandler> Update(CategoryDTO categoryDTO);
        Task<OutputHandler> DeleteApprove(CategoryDTO categoryDTO);
        Task<OutputHandler> DeleteRequest(CategoryDTO categoryDTO);
        Task<IEnumerable<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO> GetCategory(int categoryId);
    }
}
