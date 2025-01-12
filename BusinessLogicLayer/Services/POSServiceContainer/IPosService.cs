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
       Task<ShopProductDTO> Payment(JournalEntryDTO jounalEntry, List<ProductDTO> cart);
       Task<OutputHandler> NewTransaction(JournalEntryDTO jounalEntry);
        
    }
}
