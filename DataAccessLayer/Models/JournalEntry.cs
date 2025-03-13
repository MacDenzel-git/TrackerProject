using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class JournalEntry
{
    public long JournalEntryTransId { get; set; }

    public string ReceiptNo { get; set; } = null!;

    public string? ChequeNumber { get; set; }
    public string? AssociatedAccount { get; set; }

    public decimal AmountPaid { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string PayMode { get; set; } = null!;

    public string Rev { get; set; } = null!;

    public string DrCr { get; set; } = null!;

    public string ProcessedStatus { get; set; } = null!;

    public DateTime ProcessDateTime { get; set; }

    public string Processedby { get; set; } = null!;

    public string? TranscationDetails { get; set; }

    public string Revreq { get; set; } = null!;

    public string? ModifiedBy { get; set; }

    public DateTime? DateModified { get; set; }

    public int? ShopId { get; set; }

    public decimal AmountReceivedFromCustomer { get; set; }

    public decimal CashBack { get; set; }

    public int? NumberOfItemsInCart { get; set; }
}
