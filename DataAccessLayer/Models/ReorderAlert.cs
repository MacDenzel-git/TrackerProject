using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class ReorderAlert
{
    public int Id { get; set; }

    public int ProductIdentifier { get; set; }

    public int ShopId { get; set; }
}
