using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}
