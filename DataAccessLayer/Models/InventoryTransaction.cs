using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class InventoryTransaction
{
    public int TransactionId { get; set; }

    public int ShopProductId { get; set; }

    public DateTime TransactionDate { get; set; }

    public string TransactionType { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Notes { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int Rev { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DateDeleted { get; set; }

    public string? DeleteApprover { get; set; }

    public string ReceivingShop { get; set; } = null!;

    public string SendingShop { get; set; } = null!;

    public DateTime ProductExpiryDate { get; set; }

    public string ProductName { get; set; } = null!;

    public int QuantityBeforeReorder { get; set; }

    public decimal UnitPriceOfPreviousStock { get; set; }

    public decimal OrderPrice { get; set; }

    public decimal RetailPrice { get; set; }

    public string BarCode { get; set; } = null!;

    public int Sold { get; set; }

    public virtual ShopProduct ShopProduct { get; set; } = null!;
}
