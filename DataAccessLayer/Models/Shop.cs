using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Shop
{
    public int ShopId { get; set; }

    public string ShopName { get; set; } = null!;

    public int DistrictId { get; set; }

    public string ShopManagerName { get; set; } = null!;

    public string ShopManagerContact { get; set; } = null!;
}
