using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Data;

[Table("cctv", Schema = "Technic")]
public partial class Cctv
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("inventory_number")]
    public long? InventoryNumber { get; set; }

    [Column("model")]
    [StringLength(255)]
    public string Model { get; set; } = null!;

    [Column("location")]
    [StringLength(255)]
    public string? Location { get; set; }

    [Column("resolution")]
    [StringLength(10)]
    public string? Resolution { get; set; }

    [Column("ip")]
    [StringLength(15)]
    public string? Ip { get; set; }

    [Column("street")]
    public bool? Street { get; set; }

    [Column("microphone")]
    public bool? Microphone { get; set; }

    [Column("comment")]
    [StringLength(255)]
    public string? Comment { get; set; }

    [Column("deleted")]
    public bool? Deleted { get; set; }

    [Column("del_date")]
    public DateOnly? DelDate { get; set; }
}
