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
using BusinessLogicLayer.Services.InventoryTransactionsServiceContainer;
using BusinessLogicLayer.Services.OrderServiceContainer;
using BusinessLogicLayer.Services.ProductsServiceContainer;
using BusinessLogicLayer.Services.ShopProductServiceContainer;
using DataAccessLayer.UnitOfWorkContainer;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BusinessLogicLayer.Services.InventoryTransactionServiceContainer
{

    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly GenericRepository<InventoryTransaction> _inventoryTransaction;
        private readonly IShopProductService _productService;
        private UnityOfWork _unitOfWork = new UnityOfWork();

        public InventoryTransactionService(GenericRepository<InventoryTransaction> inventoryTransaction, IShopProductService productService)
        {
            _inventoryTransaction = inventoryTransaction;
            _productService = productService;
        }
        public async Task<OutputHandler> InBound(InventoryTransactionDTO inventoryTransactionDTO)
        {
            try
            {
                var mapped = new AutoMapper<InventoryTransactionDTO, InventoryTransaction>().MapToObject(inventoryTransactionDTO);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = inventoryTransactionDTO.LoggedInUsername;
                mapped.TransactionDate = DateTime.Now;
                mapped.TransactionType = "INBOUND";
                _unitOfWork.BeginTransaction();
                var product = await _unitOfWork.ShopProductRepository.GetSingleItem(x => x.ShopProductId == inventoryTransactionDTO.ShopProductId
                && x.ShopId == Convert.ToInt16(inventoryTransactionDTO.ReceivingShop));
                if (product != null)
                {
                    //Update
                    mapped.QuantityBeforeReorder = product.QuantityInStock;
                    mapped.UnitPriceOfPreviousStock = product.Price;

                    product.QuantityInStock += inventoryTransactionDTO.Quantity; //updating current in stock new figures
                    product.Price = inventoryTransactionDTO.RetailPrice; //price might change depending order price
                    product.LastOrderDate = DateTime.Now; //store last order date
                                                          // product.LastOrderDate == DateTime.Now;
                    var output = await _unitOfWork.ShopProductRepository.Update(product);
                    if (output.IsErrorOccured)
                    {
                        _unitOfWork.RollbackTransaction();
                        return output;
                    }

                }


                mapped.ProductName = product.ProductName;
                mapped.Rev = 0; //not reversed, 1 -requested, 2 confirmed/authorized
                mapped.SendingShop = "1";//HQ
                var result = await _unitOfWork.InventoryTransactionRepository.Create(mapped);
                if (result.IsErrorOccured)
                {
                    _unitOfWork.RollbackTransaction();
                }
                else
                {
                    _unitOfWork.CommitTransaction();
                }
                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }
        public async Task<OutputHandler> OutBound(InventoryTransactionDTO inventoryTransactionDTO)
        {
            try
            {

                var mapped = new AutoMapper<InventoryTransactionDTO, InventoryTransaction>().MapToObject(inventoryTransactionDTO);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = inventoryTransactionDTO.LoggedInUsername;

                var product = await _productService.GetShopProductByBranch(inventoryTransactionDTO.ShopProductId, inventoryTransactionDTO.ReceivingShop);
                if (inventoryTransactionDTO.TransactionType == "OUTBOUND") //inbound
                {

                    product.QuantityInStock += inventoryTransactionDTO.Quantity;
                }




                var result = await _inventoryTransaction.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }
        public async Task<OutputHandler> Transfer(InventoryTransferDTO inventoryTransferTransactionDTO)
        {
            try
            {

                var inventory = new InventoryTransactionDTO
                {
                    ShopProductId = inventoryTransferTransactionDTO.ShopProductId, //PRODUCT IDENTIFIER--> PRODUCT TABLE
                    TransactionDate = DateTime.Now,
                    Quantity = inventoryTransferTransactionDTO.QuantityToTransfer,
                    Notes = inventoryTransferTransactionDTO.ReasonForTransfer, //reason
                    SendingShop = inventoryTransferTransactionDTO.SendingShop,
                    ReceivingShop = inventoryTransferTransactionDTO.ReceivingShop,
                    TransactionType = "TRANSFER",
                    ProductExpiryDate = inventoryTransferTransactionDTO.ProductExpiryDate,
                    RetailPrice = Convert.ToDecimal(inventoryTransferTransactionDTO.SellingPrice),
                    BarCode = inventoryTransferTransactionDTO.BarCode
                };


                var mapped = new AutoMapper<InventoryTransactionDTO, InventoryTransaction>().MapToObject(inventory);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = inventoryTransferTransactionDTO.LoggedInUsername;

                _unitOfWork.BeginTransaction();
                //move 4m shop 1
                var SendingShopProduct = await _unitOfWork.ShopProductRepository.GetSingleItem(x => x.ProductId == inventoryTransferTransactionDTO.ProductID && x.ShopId == Convert.ToInt32(inventoryTransferTransactionDTO.SendingShop));
                SendingShopProduct.QuantityInStock -= inventoryTransferTransactionDTO.QuantityToTransfer; //remove stock from sending shop
                var sendingUpdate = await _unitOfWork.ShopProductRepository.Update(SendingShopProduct);
                if (sendingUpdate.IsErrorOccured)
                {
                    _unitOfWork.RollbackTransaction();
                    return sendingUpdate;
                }

                //add shop 2
                var ReceivinShopProduct = await _unitOfWork.ShopProductRepository.GetSingleItem(x => x.ProductId == inventoryTransferTransactionDTO.ProductID && x.ShopId == Convert.ToInt32(inventoryTransferTransactionDTO.ReceivingShop));
                if (ReceivinShopProduct is null)
                {
                    _unitOfWork.RollbackTransaction();
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "Product is not current being Sold, Please add Product to this shop and try again"
                    };
                }


                ReceivinShopProduct.QuantityInStock += inventoryTransferTransactionDTO.QuantityToTransfer; //add stock to receiving shop
                var receivingUpdate = await _unitOfWork.ShopProductRepository.Update(ReceivinShopProduct);
                if (receivingUpdate.IsErrorOccured)
                {
                    _unitOfWork.RollbackTransaction();
                    return receivingUpdate;
                }

                mapped.ProductName = SendingShopProduct.ProductName;
                var result = await _unitOfWork.InventoryTransactionRepository.Create(mapped);
                if (result.IsErrorOccured)
                {
                    _unitOfWork.RollbackTransaction();
                    return result;
                }
                _unitOfWork.CommitTransaction();
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StandardMessages.getExceptionMessage(ex);


            }

        }


        public async Task<OutputHandler> DeleteRequest(InventoryTransactionDTO inventoryTransactionDTO)
        {

            try
            {
                var inventoryTransaction = await GetInventoryTransaction(inventoryTransactionDTO.TransactionId);
                inventoryTransaction.DeletedBy = inventoryTransactionDTO.LoggedInUsername;

                var mapped = new AutoMapper<InventoryTransactionDTO, InventoryTransaction>().MapToObject(inventoryTransaction);
                var output = await _inventoryTransaction.Update(mapped);
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


        public async Task<OutputHandler> DeleteApprove(InventoryTransactionDTO inventoryTransactionDTO)
        {

            try
            {
                var inventoryTransaction = await GetInventoryTransaction(inventoryTransactionDTO.TransactionId);
                inventoryTransaction.IsDeleted = true;
                inventoryTransaction.DeleteApprover = inventoryTransactionDTO.LoggedInUsername;
                inventoryTransaction.DateDeleted = DateTime.Now;
                var mapped = new AutoMapper<InventoryTransactionDTO, InventoryTransaction>().MapToObject(inventoryTransaction);
                var output = await _inventoryTransaction.Update(mapped);
                if (output.IsErrorOccured)
                {
                    return output;
                }

                var SendingShopProduct = await _productService.GetShopProductByBranch(inventoryTransactionDTO.ShopProductId, inventoryTransactionDTO.SendingShop);
                SendingShopProduct.QuantityInStock -= inventoryTransactionDTO.Quantity; //remove stock from sending shop
                var sendingUpdate = await _productService.Update(SendingShopProduct);


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


        public async Task<InventoryTransactionDTO> GetInventoryTransaction(int inventoryTransactionId)
        {
            var output = await _inventoryTransaction.GetSingleItem(x => x.TransactionId == inventoryTransactionId);
            return new AutoMapper<InventoryTransaction, InventoryTransactionDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<InventoryTransactionDTO>> GetAllInventoryTransactions()
        {
            var output = await _inventoryTransaction.GetAll();
            return new AutoMapper<InventoryTransaction, InventoryTransactionDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(InventoryTransactionDTO inventoryTransactionDTO)
        {
            try
            {
                var mapped = new AutoMapper<InventoryTransactionDTO, InventoryTransaction>().MapToObject(inventoryTransactionDTO);
                mapped.ModifiedBy = inventoryTransactionDTO.LoggedInUsername;
                mapped.ModifiedDate = DateTime.Now;


                var result = await _inventoryTransaction.Update(mapped);
                return result;

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


    }

}
