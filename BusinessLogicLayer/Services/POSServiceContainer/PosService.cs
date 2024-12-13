using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.POSServiceContainer
{
    public class PosService : IPosService
    {
        private readonly TrackerDbContext _trackerDbContext;

        public PosService(TrackerDbContext trackerDbContext)
        {
            _trackerDbContext = trackerDbContext;
        }

        public async Task<ShopProductDTO> GetProduct(ProductSearchParamDTO productSearchParams)
        {
            //var product =  _trackerDbContext.InventoryTransactions.Where(x =>x.BarCode == productSearchParams.BarCode
            //&& x.ReceivingShop == productSearchParams.ShopId).Include().OrderByDescending(x => x.TransactionId).FirstOrDefaultAsync();

            var product = await (from c in _trackerDbContext.InventoryTransactions.Where(x => x.BarCode == productSearchParams.BarCode
            && x.ReceivingShop == productSearchParams.ShopId).OrderByDescending(x => x.TransactionId) 
            join  sp in _trackerDbContext.ShopProducts on c.ShopProductId equals sp.ShopProductId
                                 select new ShopProductDTO
                                 {
                                     ProductName = sp.ProductName,
                                     Price = sp.Price,
                                     CreatedDate = c.CreatedDate

                                 }).LastOrDefaultAsync();
            return product;
        }

        public Task<ShopProductDTO> Payment(ProductSearchParamDTO productSearchParams)
        {
            throw new NotImplementedException();
        }
    }
}
