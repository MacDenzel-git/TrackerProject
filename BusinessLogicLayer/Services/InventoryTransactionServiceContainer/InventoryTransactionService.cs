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

namespace BusinessLogicLayer.Services.InventoryTransactionServiceContainer
{

    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly GenericRepository<InventoryTransaction> _inventoryTransaction;
        private readonly IShopProductService _productService;

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
                mapped.TransactionType = "INBOUND";
                var product = await _productService.GetShopProductByBranch(inventoryTransactionDTO.ProductId, inventoryTransactionDTO.ReceivingShop);
                if (product != null)
                {
                    product.QuantityInStock += inventoryTransactionDTO.Quantity;
                   // product.LastOrderDate == DateTime.Now;
                     await _productService.Update(product);

                }




                var result = await _inventoryTransaction.Create(mapped);

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

                var product = await _productService.GetShopProductByBranch(inventoryTransactionDTO.ProductId, inventoryTransactionDTO.ReceivingShop);
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
                    ProductId = inventoryTransferTransactionDTO.ProductId, //PRODUCT IDENTIFIER--> PRODUCT TABLE
                    TransactionDate = DateTime.Now, 
                    Quantity = inventoryTransferTransactionDTO.Quantity,
                    Notes = inventoryTransferTransactionDTO.ReasonForTransfer, //reason
                     SendingShop = inventoryTransferTransactionDTO.SendingShop, 
                     ReceivingShop = inventoryTransferTransactionDTO.ReceivingShop,
                     TransactionType = "TRANSFER",
                     ProductExpiryDate = inventoryTransferTransactionDTO.ProductExpiryDate
                };


                var mapped = new AutoMapper<InventoryTransactionDTO, InventoryTransaction>().MapToObject(inventory);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = inventoryTransferTransactionDTO.LoggedInUsername;
                

                //move 4m shop 1
                var SendingShopProduct = await _productService.GetShopProductByBranch(inventoryTransferTransactionDTO.ProductId, inventoryTransferTransactionDTO.SendingShop);
                SendingShopProduct.QuantityInStock -= inventoryTransferTransactionDTO.Quantity; //remove stock from sending shop
                var sendingUpdate = await _productService.Update(SendingShopProduct);

                //add shop 2
                var ReceivinShopProduct = await _productService.GetShopProductByBranch(inventoryTransferTransactionDTO.ProductId, inventoryTransferTransactionDTO.ReceivingShop);
                ReceivinShopProduct.QuantityInStock += inventoryTransferTransactionDTO.Quantity; //add stock to receiving shop
                var receivingUpdate = await _productService.Update(ReceivinShopProduct);



                var result = await _inventoryTransaction.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
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

                var SendingShopProduct = await _productService.GetShopProductByBranch(inventoryTransactionDTO.ProductId, inventoryTransactionDTO.SendingShop);
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
