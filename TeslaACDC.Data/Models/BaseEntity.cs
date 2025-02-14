using System;
using System.ComponentModel.DataAnnotations;

namespace TeslaACDC.Data.Models;

public class BaseEntity<TId>
where TId: struct
{
    [Key]
    public TId Id {get;set;}
}