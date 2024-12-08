using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class SupplierDTO : UserInformationDTO
    {
        public int SupplierId { get; set; }

        public string CompanyName { get; set; } = null!;

        public string? ContactName { get; set; }

        public string? ContactEmail { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string? DeleteApprover { get; set; }
        public string? DeletedBy { get; set; }
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
