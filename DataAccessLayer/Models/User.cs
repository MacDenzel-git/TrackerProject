using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class User
{
    public int Id { get; set; }

    public int ShopId { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool? EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? PhoneNumberConfirmed { get; set; }

    public bool? TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int? AccessFailedCount { get; set; }

    public int RoleId { get; set; }

    public string? Salt { get; set; }

    public bool? TriggerPasswordChange { get; set; }
}
