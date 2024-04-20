using System;
using System.Collections.Generic;

namespace PizzaStore.Models;

public partial class PizzaType
{
    public string PizzaType1 { get; set; } = null!;

    public string? Name { get; set; }

    public string? Category { get; set; }

    public string? Ingredients { get; set; }
}
