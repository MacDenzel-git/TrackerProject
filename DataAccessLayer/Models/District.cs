using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class District
{
    public int DistrictId { get; set; }

    public string DistrictName { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;
}
