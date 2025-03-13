using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class SalesBarChartDTO
    {
        public IEnumerable<BarChartData> Data { get; set; }
        public List<string> MonthNames { get; set; }
        public List<string> Shops { get; set; }
    }

    public class BarChartData
    {
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public string ShopName { get; set; }
        public int TransactionCount { get; set; }
    }


}
