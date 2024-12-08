using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.ProductsServiceContainer
{
    public interface IProductService
    {
        Task<OutputHandler> Create(ProductDTO productDTO);
        Task<OutputHandler> Update(ProductDTO productDTO);
        Task<OutputHandler> DeleteApprove(ProductDTO productDTO);
        Task<OutputHandler> DeleteRequest(ProductDTO productDTO);
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProduct(int productId);
        Task<ProductDTO> GetProductByBranch(int productId, string shop);
    }
}
