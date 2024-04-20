using System;
using System.Collections.Generic;

namespace PizzaStore.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? Date { get; set; }

    public string? Time { get; set; }
}
