using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class TransactionType
{
    public int TransactionTypeId { get; set; }

    public string? TransactionTypeName { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
}
