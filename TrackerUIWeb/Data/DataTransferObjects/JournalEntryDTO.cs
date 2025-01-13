using DataAccessLayer.DataTransferObjects;

namespace TrackerUIWeb.Data.DataTransferObjects
{
    public class JournalEntryDTO : UserInformationDTO
    {
        public List<CartItemsDTO> CartItems { get; set; }

        public long JournalEntryTransId { get; set; }

        public string ReceiptNo { get; set; }

        public string? ChequeNumber { get; set; }

        public decimal AmountPaid { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string PayMode { get; set; }

        public string Rev { get; set; }

        public string DrCr { get; set; }

        public string ProcessedStatus { get; set; }

        public DateTime ProcessDateTime { get; set; }

        public string Processedby { get; set; }

        public string? TranscationDetails { get; set; }

        public string Revreq { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? DateModified { get; set; }

        public int? ShopId { get; set; }

        public decimal AmountReceivedFromCustomer { get; set; }

        public decimal CashBack { get; set; }

    }
}
