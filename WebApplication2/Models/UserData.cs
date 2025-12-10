using System;
using System.Collections.Generic;

namespace KursBus2.Models;

public partial class UserData
{
    public string? Email { get; set; }

    public string? PassWord { get; set; }

    public int UserId { get; set; }

    public string? UserName { get; set; }
}
