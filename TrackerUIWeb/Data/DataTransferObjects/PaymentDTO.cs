using DocumentFormat.OpenXml.Drawing.Charts;

namespace TrackerUIWeb.Data.DataTransferObjects
{
    public class PaymentDTO
    {
        public decimal AmountExpected { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal CashBack { get; set; }
        
    }
}
