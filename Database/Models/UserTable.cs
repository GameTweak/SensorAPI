using System;
using System.Collections.Generic;

namespace SensorAPI.Database.Models;

public partial class UserTable
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
}
