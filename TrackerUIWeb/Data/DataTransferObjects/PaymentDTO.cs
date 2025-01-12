using DocumentFormat.OpenXml.Drawing.Charts;

namespace TrackerUIWeb.Data.DataTransferObjects
{
    public class PaymentDTO
    {
        public Double AmountExpected { get; set; }
        public Double AmountPaid { get; set; }
        public Double CashBack { get; set; }
        public Double JournalEntryId { get; set; }
    }
}
