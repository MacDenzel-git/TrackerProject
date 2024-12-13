using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class ProductSearchParamDTO
    {
        public string BarCode { get; set; }
        public string ShopId { get; set; }
    }
}
