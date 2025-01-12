using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public int JournalEntryId { get; set; }

    public int ProductId { get; set; }

    public int Quanity { get; set; }

    public double Price { get; set; }

    public string? ReceiptNumber { get; set; }
}
