using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class InventoryTransaction
{
    public int TransactionId { get; set; }

    public int ProductId { get; set; }

    public DateTime TransactionDate { get; set; }

    public string TransactionType { get; set; } = null!;

    public int Quantity { get; set; }

    public string Notes { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int Rev { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DateDeleted { get; set; }

    public string? DeleteApprover { get; set; }

    public string ReceivingShop { get; set; } = null!;

    public string SendingShop { get; set; } = null!;

    public DateTime ProductExpiryDate { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual TransactionType TransactionTypeNavigation { get; set; } = null!;
}
