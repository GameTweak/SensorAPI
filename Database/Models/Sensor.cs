using System;
using System.Collections.Generic;

namespace SensorAPI.Database.Models;

public partial class Sensor
{
    public int SensorId { get; set; }

    public string SensorName { get; set; } = null!;

    public DateTime DateInstalled { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<DataTable> DataTables { get; set; } = new List<DataTable>();

    public virtual UserTable? User { get; set; }
}
