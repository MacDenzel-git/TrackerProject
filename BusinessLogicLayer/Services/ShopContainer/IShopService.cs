using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.ShopServiceContainer
{
    public interface IShopService
    {
        Task<OutputHandler> Create(ShopDTO shopDTO);
        Task<OutputHandler> Update(ShopDTO shopDTO);
        Task<OutputHandler> DeleteApprove(ShopDTO shopDTO);
        Task<OutputHandler> DeleteRequest(ShopDTO shopDTO);
        Task<IEnumerable<ShopDTO>> GetAllShops();
        Task<ShopDTO> GetShop(int shopId);
    }
}
