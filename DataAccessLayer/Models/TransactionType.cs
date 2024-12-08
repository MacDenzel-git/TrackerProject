using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class TransactionType
{
    public string TransactionTypeId { get; set; } = null!;

    public string? TransactionTypeName { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public string? DeletedBy { get; set; }

    public string? DeletedApprover { get; set; }

    public DateTime? DateDeleted { get; set; }

    public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
}
