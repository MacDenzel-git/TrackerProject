using DataAccessLayer.DataTransferObjects;

namespace TrackerUIWeb.Data.DataTransferObjects
{
    public class ShopProductDTO : UserInformationDTO
    {
        public int ShopProductID { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public int? ShopId { get; set; }


        public int QuantityInStock { get; set; }

        public int? ReorderLevel { get; set; }

        public decimal Price { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? LastOrderDate { get; set; }

        public int ProductID { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public string? DeletedBy { get; set; }

        public string? DeletedApprover { get; set; }

        public DateTime? DateDeleted { get; set; }

        public int JournalEntryId { get; set; }
    }
}
