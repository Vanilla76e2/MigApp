using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MigApp.Data;

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
