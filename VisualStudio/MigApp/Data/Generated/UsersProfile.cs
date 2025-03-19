using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MigApp.Data;

[Table("users_profiles", Schema = "Misc")]
public partial class UsersProfile
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(255)]
    public string Username { get; set; } = null!;

    [Column("user_password")]
    [StringLength(255)]
    public string? UserPassword { get; set; }

    [Column("role")]
    public int Role { get; set; }

    [Column("employee_id")]
    public int? EmployeeId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    [ForeignKey("Role")]
    [InverseProperty("UsersProfiles")]
    public virtual Role RoleNavigation { get; set; } = null!;
}
