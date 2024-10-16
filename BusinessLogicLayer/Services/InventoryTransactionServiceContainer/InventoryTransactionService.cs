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

namespace BusinessLogicLayer.Services.InventoryTransactionServiceContainer
{

    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly GenericRepository<InventoryTransaction> _inventoryTransaction;
        public InventoryTransactionService(GenericRepository<InventoryTransaction> inventoryTransaction)
        {
            _inventoryTransaction = inventoryTransaction;
        }
        public async Task<OutputHandler> Create(InventoryTransactionDTO inventoryTransactionDTO)
        {
            try
            {
                var mapped = new AutoMapper<InventoryTransactionDTO, InventoryTransaction>().MapToObject(inventoryTransactionDTO);
                mapped.CreatedDate = DateTime.Now;
                mapped.CreatedBy = inventoryTransactionDTO.LoggedInUsername;
                var result = await _inventoryTransaction.Create(mapped);

                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        //Code for deleting record
        public async Task<OutputHandler> Delete(int inventoryTransactionId)
        {

            try
            {
                var output = await _inventoryTransaction.Delete(x => x.TransactionId == inventoryTransactionId);
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
