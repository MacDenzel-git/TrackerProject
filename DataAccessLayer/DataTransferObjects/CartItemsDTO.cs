namespace DataAccessLayer.DataTransferObjects
{
    public class CartItemsDTO:UserInformationDTO
    {
        public int CartItemId { get; set; }
        public int Id { get; set; }

        public int JournalEntryId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public int Discount { get; set; }

        public decimal Price { get; set; }

        public string? ReceiptNumber { get; set; }

        public bool? Iscomplete { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? ShopId { get; set; }

        public bool? IsReversed { get; set; }

        public string? ReversedBy { get; set; }

        public DateTime? ReversalDate { get; set; }

        public string? ProductName { get; set; }
    }
}