using System;
using System.Collections.Generic;

namespace CafeSolution.Models;

public partial class Document
{
    public int Id { get; set; }

    public string ContractScan { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public virtual Employee IdNavigation { get; set; } = null!;
}
