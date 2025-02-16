using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class ShopViewModel
    {
        public decimal TodaySummation { get; set; }
        public int ExpiredItems { get; set; }
        public int LowStock { get; set; }
        public IEnumerable<InventoryTransactionDTO> InventoryTransactionList { get; set; }
        public IEnumerable<JournalEntryDTO> Transactions {get; set;}
        public IEnumerable<ShopProductDTO> ProductList {get; set;}
 
    }
}
