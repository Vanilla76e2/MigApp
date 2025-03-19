using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MigApp.Data;

[Table("roles", Schema = "Misc")]
public partial class Role
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("is_administrator")]
    public bool IsAdministrator { get; set; }

    [Column("employees_accesslevel")]
    public int EmployeesAccesslevel { get; set; }

    [Column("technics_accesslevel")]
    public int TechnicsAccesslevel { get; set; }

    [Column("furniture_accesslevel")]
    public int FurnitureAccesslevel { get; set; }

    [Column("role_name")]
    [StringLength(255)]
    public string RoleName { get; set; } = null!;

    [InverseProperty("RoleNavigation")]
    public virtual ICollection<UsersProfile> UsersProfiles { get; set; } = new List<UsersProfile>();
}
