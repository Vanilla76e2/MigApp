using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Infrastructure.Data.Entities;

[Table("departments", Schema = "Employees")]
[Index("DepartmentName", Name = "departments_department_name_key", IsUnique = true)]
public partial class Department
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("department_name")]
    [StringLength(100)]
    public string DepartmentName { get; set; } = null!;

    [InverseProperty("Department")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
