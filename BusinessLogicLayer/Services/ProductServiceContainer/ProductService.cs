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
using BusinessLogicLayer.Services.ProductsServiceContainer;
using BusinessLogicLayer.Services.OrderServiceContainer;
using BusinessLogicLayer.Services.OrderDetailServiceContainer;
using BusinessLogicLayer.Services.TransactionTypeServiceContainer;

namespace BusinessLogicLayer.Services.ProductServiceContainer
{

    public class ProductService : IProductService
    {
        private readonly GenericRepository<Product> _product;
        public ProductService(GenericRepository<Product> product)
        {
            _product = product;
        }
        public async Task<OutputHandler> Create(ProductDTO productDTO)
        {
            try
            {
                var mapped = new AutoMapper<ProductDTO, Product>().MapToObject(productDTO);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = productDTO.LoggedInUsername;
                var result = await _product.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        //Code for deleting record
        public async Task<OutputHandler> Delete(int productId)
        {

            try
            {
                var output = await _product.Delete(x => x.ProductId == productId);
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

        public async Task<ProductDTO> GetProduct(int productId)
        {
            var output = await _product.GetSingleItem(x => x.ProductId == productId);
            return new AutoMapper<Product, ProductDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var output = await _product.GetAll();
            return new AutoMapper<Product, ProductDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(ProductDTO productDTO)
        {
            try
            {
                var mapped = new AutoMapper<ProductDTO, Product>().MapToObject(productDTO);
                mapped.ModifiedBy = productDTO.LoggedInUsername;
                mapped.ModifiedDate = DateTime.Now;


                var result = await _product.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


    }

}
