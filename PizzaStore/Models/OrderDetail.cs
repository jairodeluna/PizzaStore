using System;
using System.Collections.Generic;

namespace PizzaStore.Models;

public partial class OrderDetail
{
    public int OrderDetailsId { get; set; }

    public int? OrderId { get; set; }

    public string? PizzaId { get; set; }

    public int? Quntity { get; set; }
}
