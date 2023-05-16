using System;
using System.Collections.Generic;

namespace TheKeeperProWebAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Mail { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool OnBlackList { get; set; } = false;

    public string Telephone { get; set; } = null!;

    public DateTime DateOfBorn { get; set; }

    public string PasportSeries { get; set; } = null!;

    public string PasportNumber { get; set; } = null!;

    public byte[]? VisitersPhote { get; set; }

    public byte[]? PhotoOfPasport { get; set; }

    public virtual ICollection<ListOfVisiter> ListOfVisiters { get; set; } = new List<ListOfVisiter>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
