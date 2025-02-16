using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class ProductDTO : UserInformationDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public int? CategoryId { get; set; }
        public int? DiscountQuantity { get; set; }
        public int? DiscountPercent { get; set; }


        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public string? DeletedBy { get; set; }

        public string? DeletedApprover { get; set; }

        public DateTime? DateDeleted { get; set; }



    }
}
