using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.InventoryTransactionsServiceContainer
{
    public interface IInventoryTransactionService
    {
        Task<OutputHandler> InBound(InventoryTransactionDTO inventoryTransactionDTO);
        Task<OutputHandler> OutBound(InventoryTransactionDTO inventoryTransactionDTO);
        Task<OutputHandler> Transfer(InventoryTransferDTO inventoryTransactionDTO);
        Task<OutputHandler> Update(InventoryTransactionDTO inventoryTransactionDTO);
        Task<OutputHandler> DeleteApprove(InventoryTransactionDTO inventoryTransactionDTO);
        Task<OutputHandler> DeleteRequest(InventoryTransactionDTO inventoryTransactionDTO);
        Task<IEnumerable<InventoryTransactionDTO>> GetAllInventoryTransactions();
        Task<InventoryTransactionDTO> GetInventoryTransaction(int inventoryTransactionId);
    }
}
