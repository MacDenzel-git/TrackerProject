using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{

    public class ShopSales
    {
        public decimal Amount { get; set; }
        public string ShopName { get; set; }
        public int TransactionCount { get; set; }
    }

    public class SalesPieChartDTO
    {
        public IEnumerable<ShopSales> Sales { get; set; }
        public List<string> Shops { get; set; }
    }


}
