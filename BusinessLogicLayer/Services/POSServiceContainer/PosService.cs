using AllinOne.DataHandlers;
using AutoMapper;
using BusinessLogicLayer.Services.ProductServiceContainer;
using BusinessLogicLayer.Services.ProductsServiceContainer;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepository;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWorkContainer;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogicLayer.Services.POSServiceContainer
{
    public class PosService : IPosService
    {
        private readonly TrackerDbContext _trackerDbContext;
        private readonly GenericRepository<ShopProduct> _shopProductService;
        private readonly GenericRepository<Product> _productService;
        private readonly GenericRepository<JournalEntry> _jentryService;
        private readonly GenericRepository<Shop> _shopService;
        private ILogger<PosService> _logger;

        private readonly GenericRepository<CartItem> _cartService;
        private readonly GenericRepository<InventoryTransaction> _inventoryService;
        private UnityOfWork _unityOfWork;
        public PosService(GenericRepository<Product> productService, GenericRepository<InventoryTransaction> inventoryService, TrackerDbContext trackerDbContext, GenericRepository<ShopProduct> ShopProductService, GenericRepository<JournalEntry> jentryService, GenericRepository<Shop> shopService, GenericRepository<CartItem> cartService, ILogger<PosService> logger, UnityOfWork unityOfWork)
        {
            _trackerDbContext = trackerDbContext;
            _shopProductService = ShopProductService;
            _jentryService = jentryService;
            _shopService = shopService;
            _cartService = cartService;
            _inventoryService = inventoryService;
            _logger = logger;
            _productService = productService;
            _unityOfWork = unityOfWork;
        }

        public async Task<ShopProductDTO> GetProduct(ProductSearchDTO productSearchParams)
        {
            try
            {
                //var product =  _trackerDbContext.InventoryTransactions.Where(x =>x.BarCode == productSearchParams.BarCode
                //&& x.ReceivingShop == productSearchParams.ShopId).Include().OrderByDescending(x => x.TransactionId).FirstOrDefaultAsync();
                var product = await _productService.GetSingleItem(x => x.ProductId == productSearchParams.ProductCode);
                if (product == null)
                {
                    throw new Exception("Product returned null");
                }
                
                var shopProduct = new AutoMapper<ShopProduct, ShopProductDTO>().
                    MapToObject(await _shopProductService.GetSingleItem(x => x.ShopId == productSearchParams.ShopId
                && x.ProductId == productSearchParams.ProductCode));
                if (shopProduct == null)
                {
                    return null;
                }
                else
                {
                    if (shopProduct.QuantityInStock >= productSearchParams.Quantity)
                    {


                        decimal totalPrice = shopProduct.Price * productSearchParams.Quantity;
                        shopProduct.CartItemDiscount = 0; //no discount
                        shopProduct.CartItemPrice = totalPrice;

                        if (productSearchParams.Quantity >= product.DiscountQuantity)
                        {
                            //allow discounted price 
                            decimal decimalnumber = Convert.ToDecimal(product.DiscountPercent);
                            decimalnumber = decimalnumber / 100;
                            var percentageToDecimal = decimalnumber;
                            var discountByPercentage = totalPrice * percentageToDecimal;

                            //subtract the discount amount
                             totalPrice = totalPrice - discountByPercentage;

                            //used in the UI for displaying discount and price 
                            shopProduct.CartItemDiscount = product.DiscountPercent;
                            shopProduct.CartItemPrice = totalPrice;

                        }
                        else
                        {
                            shopProduct.CartItemDiscount = 0;

                        }

                        //add to cart 

                        var cartItem = new CartItem
                            {
                                ProductId = productSearchParams.ProductCode,
                                Price = totalPrice,
                                Quantity = productSearchParams.Quantity,
                                ReceiptNumber = productSearchParams.ReceiptNumber,
                                Iscomplete = false,
                                ShopId = productSearchParams.ShopId,
                                ProductName = shopProduct.ProductName,
                                CartItemId = productSearchParams.CartItemId,
                                IsReversed = false,
                                Discount = product.DiscountPercent

                            };
                        var cartItemCreation = await _unityOfWork.CartItemsRepository.Create(cartItem);
                        if (cartItemCreation.IsErrorOccured)
                        {
                            _unityOfWork.RollbackTransaction();
                            throw new Exception("Failed to Update Product Quantity, report issue");
                        }

                        //remove from Quantity
                        ///to remove 
                        shopProduct.QuantityInStock -= productSearchParams.Quantity;
                        var output = await _shopProductService.Update(new AutoMapper<ShopProductDTO, ShopProduct>().MapToObject(shopProduct));
                        if (output.IsErrorOccured)
                        {
                            _unityOfWork.RollbackTransaction();

                            throw new Exception("Failed to Update Product Quantity, report issue");
                        }

                         

                    }
                }


                return shopProduct;
            }
            catch (Exception ex)
            {
                _unityOfWork.RollbackTransaction();
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
                 
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jounalEntry);
                _logger.LogInformation(jsonString);
                _unityOfWork.BeginTransaction();

                foreach (var item in jounalEntry.CartItems)
                {
                    //get items in chart and update them
                    var mapped = await _unityOfWork.CartItemsRepository.GetSingleItem(X => X.ReceiptNumber == jounalEntry.ReceiptNo && X.CartItemId  == item.CartItemId);
                    mapped.Iscomplete = true;
                    mapped.DateCreated = DateTime.Now;
                    

                    var output = await _unityOfWork.CartItemsRepository.Update(mapped);
                    if (output.IsErrorOccured)
                    {

                        _unityOfWork.RollbackTransaction();
                        return output;
                    }

                     
                }


                //store payment 
                //get payment 

                var payment = await _unityOfWork.JournalEntryRepository.GetSingleItem(x => x.ReceiptNo == jounalEntry.ReceiptNo);
                payment.PayMode = jounalEntry.PayMode;
                payment.AmountPaid = jounalEntry.AmountPaid;
                payment.NumberOfItemsInCart = jounalEntry.NumberOfCartItems;
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
                payment.NumberOfItemsInCart = jounalEntry.CartItems.Count();

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
                return new OutputHandler { IsErrorOccured = true, Message = ex.Message };
            }
        }

        public async Task<OutputHandler> RemoveFromCart(CartItemsDTO cartItem)
        {

            var product = new AutoMapper<ShopProduct, ShopProductDTO>().
                  MapToObject(await _shopProductService.GetSingleItem(x => x.ShopId == cartItem.ShopId
              && x.ProductId == cartItem.ProductId));
            if (product == null)
            {
                return null;
            }
            else
            {

                //add to cart 


                var cartItemCreation = await _unityOfWork.CartItemsRepository.GetSingleItem(x => x.CartItemId == cartItem.CartItemId && x.ProductId== cartItem.ProductId && x.ReceiptNumber == cartItem.ReceiptNumber);
                if (cartItemCreation != null)
                {
                    cartItemCreation.ReversalDate = DateTime.Now;
                    cartItemCreation.ReversedBy = cartItem.LoggedInUsername;
                    cartItemCreation.IsReversed = true;


                    var cartItemOutput = await _unityOfWork.CartItemsRepository.Update(cartItemCreation);
                    if (cartItemOutput.IsErrorOccured)
                    {
                        _unityOfWork.RollbackTransaction();
                        throw new Exception("Failed to Update Product Quantity, report issue");
                    }
                  
                }


                //remove from Quantity
                ///to remove 
                product.QuantityInStock += cartItem.Quantity;
                var output = await _shopProductService.Update(new AutoMapper<ShopProductDTO, ShopProduct>().MapToObject(product));
                if (output.IsErrorOccured)
                {
                    _unityOfWork.RollbackTransaction();

                    throw new Exception("Failed to Update Product Quantity, report issue");
                }


            }



            return new OutputHandler { IsErrorOccured = false, Message = "Item Removed" };

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

        public async Task<IEnumerable<JournalEntryDTO>> GetAllTransactions(int shopId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("shopId", shopId);
            var output = await _jentryService.FromSprocAsync<JournalEntryDTO>("spGetShopTransactions", parameters);
            return output;
        }

        public async Task<ShopViewModel> Dashboard(int shopId)
        {
            var jentry = await _jentryService.GetListAsync(x => x.ShopId == shopId && x.ProcessDateTime.Date == DateTime.Now.Date);
            var Inventory = await _inventoryService.GetListAsync(x=> x.ReceivingShop == shopId.ToString());
            var shopProducts = await _shopProductService.GetListAsync(x => x.ShopId == shopId);
            var shopView = new ShopViewModel
            {
                TodaySummation = jentry.Sum(i => i.AmountPaid),
                ExpiredItems = Inventory.Where(x => x.ProductExpiryDate.Date < DateTime.Now.Date).Count(),
                InventoryTransactionList = new AutoMapper<InventoryTransaction, InventoryTransactionDTO>().MapToList(Inventory.OrderByDescending(x
                => x.CreatedDate).Take(20)),
                LowStock = shopProducts.Where(x => x.ReorderLevel > x.QuantityInStock).Count(),
                Transactions = new AutoMapper<JournalEntry, JournalEntryDTO>().MapToList(jentry),
                ProductList = new AutoMapper<ShopProduct, ShopProductDTO>().MapToList(shopProducts)

            };
            return shopView;

        }

        public async Task<IEnumerable<ShopProductDTO>> GetLowStockProduct(int shopId)
        {
            var shopProducts = new AutoMapper<ShopProduct, ShopProductDTO>().MapToList(await _shopProductService.GetListAsync(x => x.ShopId == shopId && x.ReorderLevel > x.QuantityInStock));
            return shopProducts;
        }

        public async Task<IEnumerable<InventoryTransactionDTO>> GetExpiredInventory(int shopId)
        {
            var Inventory = new AutoMapper<InventoryTransaction, InventoryTransactionDTO>().MapToList(await _inventoryService.GetListAsync(x => x.ReceivingShop == shopId.ToString() && x.ProductExpiryDate.Date < DateTime.Now.Date));
            return Inventory;
            
        }

        public async Task<IEnumerable<CartItemsDTO>> GetCartItems(string receipt)
        {
            var cartItems = await _cartService.GetListAsync(x => x.ReceiptNumber == receipt && x.IsReversed == false);
            return new AutoMapper<CartItem, CartItemsDTO>().MapToList(cartItems);
        }
    }
}
