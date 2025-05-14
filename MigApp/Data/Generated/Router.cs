using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Data;

[Table("routers", Schema = "Technic")]
public partial class Router
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("inventory_number")]
    public long? InventoryNumber { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("model")]
    [StringLength(255)]
    public string Model { get; set; } = null!;

    [Column("location")]
    [StringLength(255)]
    public string? Location { get; set; }

    [Column("ip")]
    [StringLength(15)]
    public string? Ip { get; set; }

    [Column("dhcp")]
    [StringLength(19)]
    public string? Dhcp { get; set; }

    [Column("admin_login")]
    [StringLength(255)]
    public string? AdminLogin { get; set; }

    [Column("admin_password")]
    [StringLength(255)]
    public string? AdminPassword { get; set; }

    [Column("wifi_name")]
    [StringLength(255)]
    public string? WifiName { get; set; }

    [Column("wifi_password")]
    [StringLength(255)]
    public string? WifiPassword { get; set; }

    [Column("comment")]
    [StringLength(255)]
    public string? Comment { get; set; }

    [Column("deleted")]
    public bool Deleted { get; set; }

    [Column("del_date")]
    public DateTime? DelDate { get; set; }
}
