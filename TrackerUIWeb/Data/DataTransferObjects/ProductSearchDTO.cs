using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerUIWeb.Data.DataTransferObjects
{
    public class ProductSearchDTO
    {
        public string ProductCode { get; set; }
        public int ShopId { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public string? ReceiptNumber { get; set; }

    }
}
