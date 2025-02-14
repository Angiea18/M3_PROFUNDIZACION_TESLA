using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TeslaACDC.Data.Models;

public class Song : BaseEntity<int>
{
    public string Name { get; set; } = String.Empty;
    public int DurationSeg { get; set; }
    [ForeignKey("Album")]
    public int AlbumId { get; set; }
    [JsonIgnore]
    public virtual Album? Album { get; set; }

    public string DurationMin()
    {
        int minutos = DurationSeg / 60;
        int segundos = DurationSeg % 60;

        return $"{minutos}m {segundos}s";

    }

}