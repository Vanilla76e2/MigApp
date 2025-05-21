using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Infrastructure.Data.Entities;

[Table("employees", Schema = "Employees")]
public partial class Employee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fio")]
    [StringLength(255)]
    public string Fio { get; set; } = null!;

    [Column("department_id")]
    public int DepartmentId { get; set; }

    [Column("workplace")]
    [StringLength(50)]
    public string? Workplace { get; set; }

    [Column("deleted")]
    public bool Deleted { get; set; }

    [Column("del_date")]
    public DateOnly? DelDate { get; set; }

    [Column("comment")]
    [StringLength(500)]
    public string? Comment { get; set; }

    [Column("phone_number")]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("Employees")]
    public virtual Department Department { get; set; } = null!;

    [InverseProperty("Employee")]
    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();
}
