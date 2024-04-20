using System;
using System.Collections.Generic;

namespace PizzaStore.Models;

public partial class Pizza
{
    public string PizzaId { get; set; } = null!;

    public string PizzaType { get; set; } = null!;

    public string? Size { get; set; }

    public double? Price { get; set; }
}
