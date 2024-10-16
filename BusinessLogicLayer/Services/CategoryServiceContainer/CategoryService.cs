using AllinOne.DataHandlers.ErrorHandler;
using AllinOne.DataHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepository;

namespace BusinessLogicLayer.Services.CategoryServiceContainer
{

    public class CategoryService : ICategoryService
    {
        private readonly GenericRepository<Category> _category;
        public CategoryService(GenericRepository<Category> category)
        {
            _category = category;
        }
        public async Task<OutputHandler> Create(CategoryDTO categoryDTO)
        {
            try
            {
                var mapped = new AutoMapper<CategoryDTO, Category>().MapToObject(categoryDTO);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = categoryDTO.LoggedInUsername;
                var result = await _category.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        //Code for deleting record
        public async Task<OutputHandler> Delete(int categoryId)
        {

            try
            {
                var output = await _category.Delete(x => x.CategoryId == categoryId);
                if (output.IsErrorOccured)
                {
                    return output;
                }
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = StandardMessages.GetSuccessfulMessage() //assign message to the error
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<CategoryDTO> GetCategory(int categoryId)
        {
            var output = await _category.GetSingleItem(x => x.CategoryId == categoryId);
            return new AutoMapper<Category, CategoryDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var output = await _category.GetAll();
            return new AutoMapper<Category, CategoryDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(CategoryDTO categoryDTO)
        {
            try
            {
                var mapped = new AutoMapper<CategoryDTO, Category>().MapToObject(categoryDTO);
                mapped.ModifiedBy = categoryDTO.LoggedInUsername;
                mapped.ModifiedDate = DateTime.Now;


                var result = await _category.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


    }

}
