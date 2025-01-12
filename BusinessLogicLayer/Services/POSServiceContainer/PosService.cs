using AllinOne.DataHandlers;
using BusinessLogicLayer.Services.ProductsServiceContainer;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepository;
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
        private readonly GenericRepository<ShopProduct> _productService;
        private readonly GenericRepository<JournalEntry> _jentryService;
        private readonly GenericRepository<Shop> _shopService;
        public PosService(TrackerDbContext trackerDbContext, GenericRepository<ShopProduct> productService, GenericRepository<JournalEntry> jentryService, GenericRepository<Shop> shopService)
        {
            _trackerDbContext = trackerDbContext;
            _productService = productService;
            _jentryService = jentryService;
            _shopService = shopService;
        }

        public async Task<ShopProductDTO> GetProduct(ProductSearchDTO productSearchParams)
        {
            //var product =  _trackerDbContext.InventoryTransactions.Where(x =>x.BarCode == productSearchParams.BarCode
            //&& x.ReceivingShop == productSearchParams.ShopId).Include().OrderByDescending(x => x.TransactionId).FirstOrDefaultAsync();
            
            var product = new AutoMapper<ShopProduct, ShopProductDTO>().MapToObject(await _productService.GetSingleItem(x => x.ShopProductId == productSearchParams.ShopId 
            && x.ProductId == productSearchParams.ProductCode));
            return product;
        }

        public async Task<OutputHandler> NewTransaction(JournalEntryDTO jounalEntry)
        {
            var jentry = new JournalEntryDTO
            {
                Processedby = jounalEntry.Processedby,
                ReceiptNo = await GetReceipt(jounalEntry.ShopId),
                Rev = "-1",
                DrCr = "C",
                ProcessedStatus = "0",
                TranscationDetails = "Transaction Started",
                Revreq = "0",
                ShopId = jounalEntry.ShopId,
                CreatedDateTime = DateTime.Now,
                ProcessDateTime = DateTime.Now,
                PayMode= ""
                
            };

            var mapped = new AutoMapper<JournalEntryDTO, JournalEntry>().MapToObject(jentry);

         var output =  await _jentryService.Create(mapped);
            if (output.IsErrorOccured == false)
            {
                output.Result = mapped.ReceiptNo;

            }
            return output;
        }

        private async Task<string> GetReceipt(int shopId)
        {
            var receipt = await _shopService.GetSingleItem(x => x.ShopId == shopId);
            receipt.ReceiptRange = receipt.ReceiptRange + 1;
            var TransactionReceipt = $"00{shopId}{DateTime.Now.ToString("yyMMdd")}{receipt.ReceiptRange}";
            return TransactionReceipt;
        }

        public Task<ShopProductDTO> Payment(JournalEntryDTO jounalEntry, List<ProductDTO> cart)
        {
            throw new NotImplementedException();
        }
    }
}
