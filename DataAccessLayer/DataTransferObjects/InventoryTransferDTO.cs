using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class InventoryTransferDTO : UserInformationDTO
    {
        public int ReceivingShopId { get; set; }
        public int InitiatingShopId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int BranchId { get; set; }
        public string ReasonForTransfer { get; set; }
        public string SendingShop { get; set; }
        public string ReceivingShop { get; set; }
        public string InitiatingPersonel { get; set; }
        public DateTime ProductExpiryDate { get; set; }
        public string TransferApprovalPersonel { get; set; }
    }
}
