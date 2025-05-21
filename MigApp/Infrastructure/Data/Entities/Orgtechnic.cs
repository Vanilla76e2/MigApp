using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Infrastructure.Data.Entities;

[Table("orgtechnic", Schema = "Technic")]
public partial class Orgtechnic
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("inventory_number")]
    public long? InventoryNumber { get; set; }

    [Column("type")]
    [StringLength(255)]
    public string Type { get; set; } = null!;

    [Column("model")]
    [StringLength(255)]
    public string Model { get; set; } = null!;

    [Column("serial_number")]
    [StringLength(255)]
    public string? SerialNumber { get; set; }

    [Column("ip")]
    [StringLength(15)]
    public string? Ip { get; set; }

    [Column("service_login")]
    [StringLength(255)]
    public string? ServiceLogin { get; set; }

    [Column("service_password")]
    [StringLength(255)]
    public string? ServicePassword { get; set; }

    [Column("cartridge_model")]
    [StringLength(255)]
    public string? CartridgeModel { get; set; }

    [Column("workplace")]
    [StringLength(255)]
    public string? Workplace { get; set; }

    [Column("comment")]
    [StringLength(255)]
    public string? Comment { get; set; }

    [Column("deleted")]
    public bool Deleted { get; set; }

    [Column("del_date")]
    public DateOnly? DelDate { get; set; }
}
