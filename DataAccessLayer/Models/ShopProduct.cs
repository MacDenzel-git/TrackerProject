using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class ShopProduct
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int ShopId { get; set; }

    public int StockAmount { get; set; }

    public string ProductName { get; set; } = null!;
}
