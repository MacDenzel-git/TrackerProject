using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class OrderDTO : UserInformationDTO
    {
        public int OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? SupplierId { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
