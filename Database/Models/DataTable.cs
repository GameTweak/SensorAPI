using System;
using System.Collections.Generic;

namespace SensorAPI.Database.Models;

public partial class DataTable
{
    public int DataId { get; set; }

    public int? SensorId { get; set; }

    public DateTime DateReported { get; set; }

    public decimal SensorValue { get; set; }

    public virtual Sensor? Sensor { get; set; }
}
