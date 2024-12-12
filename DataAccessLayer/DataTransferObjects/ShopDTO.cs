using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class ShopDTO : UserInformationDTO
    {
        public int ShopId { get; set; }

        public string ShopName { get; set; } = null!;

        public int DistrictId { get; set; }

        public string ShopManagerName { get; set; } = null!;

        public string ShopManagerContact { get; set; } = null!;
        public bool? IsDeleted { get; set; }

        public string? DeletedBy { get; set; }

        public string? DeletedApprover { get; set; }

        public DateTime? DateDeleted { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

    }
}
