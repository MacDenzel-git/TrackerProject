using DataAccessLayer.DataTransferObjects;

namespace TrackerUIWeb.Data.DataTransferObjects
{
    public class InventoryTransferDTO : UserInformationDTO
    {
        
        public int QuantityInStock { get; set; }
        public int QuantityToTransfer { get; set; }
        public int ShopProductId { get; set; }
        public int ProductID { get; set; }
        public DateTime ProductExpiryDate { get; set; }
        public string ReasonForTransfer { get; set; }
        public string SendingShop { get; set; }
        public string ReceivingShop { get; set; }
        public string InitiatingPersonel { get; set; }
        public string SellingPrice { get; set; }
        public string FromShopRetailPrice { get; set; } //how much the product is being sold at the current shop
        public string? TransferApprovalPersonel { get; set; }
        public string BarCode { get; set; } = null!;

        public int? Sold { get; set; }
    }
}
