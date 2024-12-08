using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class TransactionTypeDTO : UserInformationDTO
    {
        public int TransactionTypeId { get; set; }

        public string? TransactionTypeName { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }
        public string DeleteApprover { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
