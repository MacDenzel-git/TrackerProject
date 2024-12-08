using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class InventoryTransactionDTO : UserInformationDTO
    {
        public int TransactionId { get; set; }

        public int ProductId { get; set; }

        public DateTime? TransactionDate { get; set; }

        public int? TransactionTypeId { get; set; }

        public int? Quantity { get; set; }

        public string ReceivingShop { get; set; } = null!;

        public string SendingShop { get; set; } = null!;

        public DateTime ProductExpiryDate { get; set; }

        public string? Notes { get; set; }

        public string? CreatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }
        public string DeleteApprover { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int BranchId { get; set; }
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
