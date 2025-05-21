using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Infrastructure.Data.Entities;

[Table("computers", Schema = "Technic")]
public partial class Computer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("inventory_number")]
    public long? InventoryNumber { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("ip")]
    public long? Ip { get; set; }

    [Column("employee_id")]
    public int? EmployeeId { get; set; }

    [Column("operating_system")]
    [StringLength(100)]
    public string? OperatingSystem { get; set; }

    [Column("comment")]
    public string? Comment { get; set; }

    [Column("deleted")]
    public bool Deleted { get; set; }

    [Column("del_date")]
    public DateOnly? DelDate { get; set; }

    [InverseProperty("Computer")]
    public virtual ICollection<ComputersComponent> ComputersComponents { get; set; } = new List<ComputersComponent>();

    [InverseProperty("Computer")]
    public virtual ICollection<ComputersDevice> ComputersDevices { get; set; } = new List<ComputersDevice>();

    [InverseProperty("Computer")]
    public virtual ICollection<ComputersServiceHistory> ComputersServiceHistories { get; set; } = new List<ComputersServiceHistory>();

    [ForeignKey("EmployeeId")]
    [InverseProperty("Computers")]
    public virtual Employee? Employee { get; set; }
}
