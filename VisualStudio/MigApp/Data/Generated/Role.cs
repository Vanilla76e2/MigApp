using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MigApp.Core.Authorization;

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
    public RolePermission EmployeesAccesslevel { get; set; }

    [Column("technics_accesslevel")]
    public RolePermission TechnicsAccesslevel { get; set; }

    [Column("furniture_accesslevel")]
    public RolePermission FurnitureAccesslevel { get; set; }

    [Column("role_name")]
    [StringLength(255)]
    public string RoleName { get; set; } = null!;

    [InverseProperty("RoleNavigation")]
    public virtual ICollection<UsersProfile> UsersProfiles { get; set; } = new List<UsersProfile>();
}
