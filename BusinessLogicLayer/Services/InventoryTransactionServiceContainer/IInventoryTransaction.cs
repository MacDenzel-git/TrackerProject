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
        Task<OutputHandler> Create(InventoryTransactionDTO inventoryTransactionDTO);
        Task<OutputHandler> Update(InventoryTransactionDTO inventoryTransactionDTO);
        Task<OutputHandler> Delete(int InventoryTransactionId);
        Task<IEnumerable<InventoryTransactionDTO>> GetAllInventoryTransactions();
        Task<InventoryTransactionDTO> GetInventoryTransaction(int inventoryTransactionId);
    }
}
