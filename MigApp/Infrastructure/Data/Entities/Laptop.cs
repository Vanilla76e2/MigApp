using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Infrastructure.Data.Entities;

[Table("laptops", Schema = "Technic")]
public partial class Laptop
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

    [Column("employee_id")]
    public int? EmployeeId { get; set; }

    [Column("diagonal")]
    public float? Diagonal { get; set; }

    [Column("resolution")]
    [StringLength(255)]
    public string? Resolution { get; set; }

    [Column("operating_system")]
    [StringLength(255)]
    public string? OperatingSystem { get; set; }

    [Column("processor")]
    [StringLength(255)]
    public string? Processor { get; set; }

    [Column("ram")]
    [StringLength(255)]
    public string? Ram { get; set; }

    [Column("drive")]
    [StringLength(255)]
    public string? Drive { get; set; }

    [Column("other")]
    [StringLength(255)]
    public string? Other { get; set; }

    [Column("comment")]
    public string? Comment { get; set; }

    [Column("deleted")]
    public bool Deleted { get; set; }

    [Column("del_date")]
    public DateOnly? DelDate { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Laptops")]
    public virtual Employee? Employee { get; set; }
}
