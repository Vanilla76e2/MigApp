using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MigApp.Data;

[Keyless]
public partial class RolesView
{
    [Column("id")]
    public int? Id { get; set; }

    [Column("role_name")]
    [StringLength(255)]
    public string? RoleName { get; set; }

    [Column("employees_accesslevel")]
    public string? EmployeesAccesslevel { get; set; }

    [Column("technics_accesslevel")]
    public string? TechnicsAccesslevel { get; set; }

    [Column("furniture_accesslevel")]
    public string? FurnitureAccesslevel { get; set; }

    [Column("is_administrator")]
    public string? IsAdministrator { get; set; }
}
