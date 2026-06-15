using System;
using System.Collections.Generic;

namespace RetailPOS.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string? PaymentMethod { get; set; }

    public int UserId { get; set; }

    // ✅ NEW FIELDS
    public decimal? AmountReceived { get; set; }
    public decimal? ChangeAmount { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
