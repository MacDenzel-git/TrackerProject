using AllinOne.DataHandlers;
using BusinessLogicLayer.Services.ProductsServiceContainer;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepository;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWorkContainer;
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
        private readonly GenericRepository<ShopProduct> _shopProductService;
        private readonly GenericRepository<JournalEntry> _jentryService;
        private readonly GenericRepository<Shop> _shopService;
        private UnityOfWork _unityOfWork = new UnityOfWork();
        public PosService(TrackerDbContext trackerDbContext, GenericRepository<ShopProduct> productService, GenericRepository<JournalEntry> jentryService, GenericRepository<Shop> shopService)
        {
            _trackerDbContext = trackerDbContext;
            _shopProductService = productService;
            _jentryService = jentryService;
            _shopService = shopService;
        }

        public async Task<ShopProductDTO> GetProduct(ProductSearchDTO productSearchParams)
        {
            //var product =  _trackerDbContext.InventoryTransactions.Where(x =>x.BarCode == productSearchParams.BarCode
            //&& x.ReceivingShop == productSearchParams.ShopId).Include().OrderByDescending(x => x.TransactionId).FirstOrDefaultAsync();
            
            var product = new AutoMapper<ShopProduct, ShopProductDTO>().MapToObject(await _shopProductService.GetSingleItem(x => x.ShopProductId == productSearchParams.ShopId 
            && x.ProductId == productSearchParams.ProductCode));

            if (product.QuantityInStock >= productSearchParams.Quantity)
            {
                //remove from Quantity
                product.QuantityInStock -= productSearchParams.Quantity;
                var output = await _shopProductService.Update(new AutoMapper<ShopProductDTO, ShopProduct>().MapToObject(product));
                if (output.IsErrorOccured)
                {
                    throw new Exception("Failed to Update Product Quantity, report issue");
                }
            }


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

        public async Task<OutputHandler> Payment(JournalEntryDTO jounalEntry)
        {
            _unityOfWork.JournalEntryRepository.BeginTransaction();

            foreach (var item in jounalEntry.CartItems)
            {
                var mapped = new AutoMapper<CartItemsDTO, CartItem>().MapToObject(item);
                var output = await _unityOfWork.CartItemsRepository.Create(mapped);
                if (output.IsErrorOccured)
                {
                    _unityOfWork.RollbackTransaction();
                    return output;
                }
            }

            //Store Cart items 

            //store payment 
            //get payment 

            var payment = await _unityOfWork.JournalEntryRepository.GetSingleItem(x => x.ReceiptNo == jounalEntry.ReceiptNo);
            payment.PayMode = jounalEntry.PayMode;
            payment.AmountPaid = jounalEntry.AmountPaid;
            payment.Rev = "0";
            payment.Revreq = "0";
            payment.DrCr = "C";
            payment.ProcessedStatus = "1";
            payment.CreatedDateTime = DateTime.Now;
            payment.ProcessDateTime = DateTime.Now;
            payment.TranscationDetails = "Successful";
            payment.ShopId = jounalEntry.ShopId;
            payment.AmountReceivedFromCustomer = jounalEntry.AmountReceivedFromCustomer;
            payment.CashBack = jounalEntry.CashBack;
            payment.ChequeNumber = "";
            payment.Processedby = jounalEntry.LoggedInUsername;

         var paymentResult =   await _unityOfWork.JournalEntryRepository.Update(payment);
            if (paymentResult.IsErrorOccured)
            {
                _unityOfWork.RollbackTransaction();
                return paymentResult;
            }
            else
            {
                _unityOfWork.CommitTransaction();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Payment Successful"
                };
            }
        }
    }
}
