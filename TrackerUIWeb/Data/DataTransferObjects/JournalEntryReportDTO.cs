namespace TrackerUIWeb.Data.DataTransferObjects
{
    public class JournalEntryReportDTO
    {
        public string AGROReceipt { get; set; }

        public decimal AmountPaid { get; set; }

        public DateTime TransactionDate { get; set; }

        public string PaymentType { get; set; }

        public string Rev { get; set; }


        public string Processedby { get; set; }
        public string? AssociatedAccount { get; set; }

        public string? TranscationDetails { get; set; }

        public string ShopName { get; set; }

        public decimal AmountReceivedFromCustomer { get; set; }

        public decimal Change { get; set; }
        public int NumberOfCartItems { get; set; }
    }
}
