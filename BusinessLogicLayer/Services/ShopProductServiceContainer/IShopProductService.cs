using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.ShopProductServiceContainer
{
    public interface IShopProductService
    {
        Task<OutputHandler> Create(ShopProductDTO productDTO);
        Task<OutputHandler> Update(ShopProductDTO productDTO);
        Task<OutputHandler> DeleteApprove(ShopProductDTO productDTO);
        Task<OutputHandler> DeleteRequest(ShopProductDTO productDTO);
        Task<IEnumerable<ShopProductDTO>> GetAllShopProducts();
        Task<ShopProductDTO> GetShopProduct(int productId);
        Task<ShopProductDTO> GetShopProductByBranch(int productId, string shop);
        Task<ShopProductDTO> GetShopProductByBranch(int productId, int shopId);
    }
}
