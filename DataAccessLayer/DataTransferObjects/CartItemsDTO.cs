namespace DataAccessLayer.DataTransferObjects
{
    public class CartItemsDTO
    {
        public int JournalEntryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}