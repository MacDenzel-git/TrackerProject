using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class InventoryTransaction
{
    public int TransactionId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public int? TransactionTypeId { get; set; }

    public int? Quantity { get; set; }

    public string? Notes { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Product? Product { get; set; }

    public virtual TransactionType? TransactionType { get; set; }
}
