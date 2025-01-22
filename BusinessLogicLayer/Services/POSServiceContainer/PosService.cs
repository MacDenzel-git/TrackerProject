using AllinOne.DataHandlers;
using BusinessLogicLayer.Services.ProductsServiceContainer;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepository;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWorkContainer;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
        private readonly GenericRepository<CartItem> _cartService;
        private UnityOfWork _unityOfWork = new UnityOfWork();
        public PosService(TrackerDbContext trackerDbContext, GenericRepository<ShopProduct> productService, GenericRepository<JournalEntry> jentryService, GenericRepository<Shop> shopService, GenericRepository<CartItem> cartService)
        {
            _trackerDbContext = trackerDbContext;
            _shopProductService = productService;
            _jentryService = jentryService;
            _shopService = shopService;
            _cartService = cartService;
        }

        public async Task<ShopProductDTO> GetProduct(ProductSearchDTO productSearchParams)
        {
            try
            {
                //var product =  _trackerDbContext.InventoryTransactions.Where(x =>x.BarCode == productSearchParams.BarCode
                //&& x.ReceivingShop == productSearchParams.ShopId).Include().OrderByDescending(x => x.TransactionId).FirstOrDefaultAsync();

                var product = new AutoMapper<ShopProduct, ShopProductDTO>().
                    MapToObject(await _shopProductService.GetSingleItem(x => x.ShopId == productSearchParams.ShopId
                && x.ProductId == productSearchParams.ProductCode));
                if (product == null)
                {
                    return null;
                }
                else
                {
                    if (product.QuantityInStock >= productSearchParams.Quantity)
                    {
                        //add to cart 

                        var cartItem = new CartItem
                        {
                            ProductId = productSearchParams.ProductCode,
                            Price = product.Price * productSearchParams.Quantity,
                            Quantity = productSearchParams.Quantity,
                            ReceiptNumber = productSearchParams.ReceiptNumber,
                            Iscomplete = false,
                            ShopId = productSearchParams.ShopId,
                            ProductName = product.ProductName,
                        };
                        var cartItemCreation = await _unityOfWork.CartItemsRepository.Create(cartItem);
                        if (cartItemCreation.IsErrorOccured)
                        {
                            _unityOfWork.RollbackTransaction();
                            throw new Exception("Failed to Update Product Quantity, report issue");
                        }

                        //remove from Quantity
                        ///to remove 
                        product.QuantityInStock -= productSearchParams.Quantity;
                        var output = await _shopProductService.Update(new AutoMapper<ShopProductDTO, ShopProduct>().MapToObject(product));
                        if (output.IsErrorOccured)
                        {
                            _unityOfWork.RollbackTransaction();

                            throw new Exception("Failed to Update Product Quantity, report issue");
                        }
                    }
                }


                return product;
            }
            catch (Exception ex)
            {

                return null;
            }
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
                PayMode = ""

            };

            var mapped = new AutoMapper<JournalEntryDTO, JournalEntry>().MapToObject(jentry);

            var output = await _jentryService.Create(mapped);
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

            var output = await _shopService.Update(receipt);
            if (output.IsErrorOccured)
            {
                throw new Exception();
            }
            return TransactionReceipt;
        }

        public async Task<OutputHandler> Payment(JournalEntryDTO jounalEntry)
        {
            //LOG
            try
            {
                _unityOfWork.BeginTransaction();

                foreach (var item in jounalEntry.CartItems)
                {
                    var mapped = await _unityOfWork.CartItemsRepository.GetSingleItem(X=>X.ReceiptNumber ==jounalEntry.ReceiptNo);
                    mapped.Iscomplete = true;
                    mapped.DateCreated = DateTime.Now;
                    var output = await _unityOfWork.CartItemsRepository.Update(mapped);
                    if (output.IsErrorOccured)
                    {

                        _unityOfWork.RollbackTransaction();
                        return output;
                    }

                    var product = new AutoMapper<ShopProduct, ShopProductDTO>().MapToObject(await _shopProductService.GetSingleItem(x => x.ProductId == jounalEntry.ShopId
                    && x.ProductId == item.ProductId));
                    if (product.QuantityInStock >= item.Quantity)
                    {
                        //remove from Quantity
                        product.QuantityInStock -= item.Quantity;
                        var outputUpdate = await _unityOfWork.ShopProductRepository.Update(new AutoMapper<ShopProductDTO, ShopProduct>().MapToObject(product));
                        if (output.IsErrorOccured)
                        {
                            _unityOfWork.RollbackTransaction();
                            throw new Exception("Failed to Update Product Quantity, report issue");
                        }


                    }
                }

           
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

                var paymentResult = await _unityOfWork.JournalEntryRepository.Update(payment);
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
            catch (Exception ex)
            {
                _unityOfWork.RollbackTransaction();
                return new OutputHandler {IsErrorOccured =true, Message =  ex.Message} ;
            }
        }

        public Task<OutputHandler> RemoveFromCart(string Receipt, string ProductId)
        {
           // RemoveFromCart from cart

           //1. get cart item
           //2. update set is reversed true, reversed by, reversed by date 
           //update 
           //Remove from list 
            throw new NotImplementedException();
        }

        public Task<OutputHandler> ReturnProduct(string Receipt, string Product)
        {
            //Remove from Cart after checkout 
            throw new NotImplementedException();
        }
            
        public async Task<JournalEntryDTO> GetReceiptDetails(string receipt)
        {
            var output = await _jentryService.GetSingleItem(x => x.ReceiptNo == receipt);
            if (output is null)
            {
                throw new Exception("Something went wrong");
            }
            else
            {
                var mapped = new AutoMapper<JournalEntry, JournalEntryDTO>().MapToObject(output);
                var cartItems = (await _cartService.GetListAsync(x => x.ReceiptNumber == receipt));
                 List<CartItemsDTO> item = (List<CartItemsDTO>)new AutoMapper<CartItem, CartItemsDTO>().MapToList(cartItems.ToList());
                mapped.CartItems = item;
                
                return mapped;
            }
        }
    
    }
}
