using AllinOne.DataHandlers;
using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.POSServiceContainer
{
    public interface IPosService
    {
       Task<ShopProductDTO> GetProduct(ProductSearchDTO productSearchParams); 
       Task<IEnumerable<ShopProductDTO>> GetLowStockProduct(int shopId);
       Task<IEnumerable<InventoryTransactionDTO>> GetExpiredInventory(int shopId);
       Task<OutputHandler> Payment(JournalEntryDTO jounalEntry);
       Task<OutputHandler> NewTransaction(JournalEntryDTO jounalEntry);
        Task<OutputHandler> RemoveFromCart(CartItemsDTO cartItem); //before checkout
        Task<OutputHandler> ReturnProduct(string Receipt, string Product); //after checkout - not to be used, For Super Admin
        Task<JournalEntryDTO> GetReceiptDetails(string receipt);
        Task<IEnumerable<CartItemsDTO>> GetCartItems(string receipt);
        Task<IEnumerable<JournalEntryDTO>> GetAllTransactions(int shopId);
        Task<ShopViewModel> Dashboard(int shopId);
    }
}
