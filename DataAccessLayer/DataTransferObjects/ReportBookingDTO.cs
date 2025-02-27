using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObjects
{
    public class ReportBookingDTO
    {
 
        public string IncentiveDescription { get; set; }

        public decimal Price { get; set; }

        public string RoomName { get; set; }

        public int NumberOfDays { get; set; }

        public DateTime Checkin { get; set; }

        public DateTime CheckOut { get; set; }
             
        public string CustomerName { get; set; } = null!;
        public string PaymentChannelDescription { get; set; } = null!;
        public string NumberOfOccupants { get; set; } = null!;
         public int RecentConcurentVisits { get; set; }  
        public int? TotalNumberOfVisits { get; set; }  
        public string CreatedBy { get; set; } = null!;
        public string PaymentReference { get; set; } = null!;
        public string? ReversalComment { get; set; }
        public bool? IsCheckedOut { get; set; }
        public bool? IsReversed { get; set; }
    }
}
