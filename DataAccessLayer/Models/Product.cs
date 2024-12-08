using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public int? QuantityInStock { get; set; }

    public int? ReorderLevel { get; set; }

    public decimal? Price { get; set; }

    public int? ShopId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public string? DeletedBy { get; set; }

    public string? DeletedApprover { get; set; }

    public DateTime? DateDeleted { get; set; }

    public DateTime? LastOrderDate { get; set; }

    public int ProductIdentifier { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
