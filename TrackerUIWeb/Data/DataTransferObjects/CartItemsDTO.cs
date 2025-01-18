namespace TrackerUIWeb.Data.DataTransferObjects
{
    public class CartItemsDTO
    {
        public int JournalEntryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool? Iscomplete { get; set; }
        public string? ReceiptNumber { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? ShopId { get; set; }

        public string? ShopProduct { get; set; }
    }
}
