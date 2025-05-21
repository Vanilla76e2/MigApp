using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Infrastructure.Data.Entities;

[Table("monitors", Schema = "Technic")]
public partial class Monitor
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("inventory_number")]
    public long? InventoryNumber { get; set; }

    [Column("model")]
    [StringLength(255)]
    public string Model { get; set; } = null!;

    [Column("serial_number")]
    [StringLength(255)]
    public string? SerialNumber { get; set; }

    [Column("diagonal")]
    public float? Diagonal { get; set; }

    [Column("resolution")]
    [StringLength(255)]
    public string? Resolution { get; set; }

    [Column("comment")]
    [StringLength(255)]
    public string? Comment { get; set; }

    [Column("deleted")]
    public bool Deleted { get; set; }

    [Column("del_date")]
    public DateOnly? DelDate { get; set; }

    [Column("vga_port")]
    public short? VgaPort { get; set; }

    [Column("dvi_port")]
    public short? DviPort { get; set; }

    [Column("hdmi_port")]
    public short? HdmiPort { get; set; }

    [Column("dp_port")]
    public short? DpPort { get; set; }
}
