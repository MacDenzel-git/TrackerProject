using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string? RoleDescription { get; set; }
}
