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
       Task<ShopProductDTO> GetProduct(ProductSearchParamDTO productSearchParams);
       Task<ShopProductDTO> Payment(JounalEntryDTO jounalEntry, List<ProductDTO> cart);
        
    }
}
