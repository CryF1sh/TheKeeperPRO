using System;
using System.Collections.Generic;

namespace TheKeeperProWebAPI.Models;

public partial class ListOfVisiter
{
    private TheKeeperProPracticeContext db;
    public ListOfVisiter() 
    {
        db = new TheKeeperProPracticeContext();
    }
    public int ListOfVisitersId { get; set; }

    public int RequestId { get; set; }

    public int UserId { get; set; }

    public bool? Creater { get; set; }

    public virtual Request Request { get; set; } = null!;

    public virtual User? User { get; set; } = null!;
}
