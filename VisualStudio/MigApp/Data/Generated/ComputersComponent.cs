using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Data;

[Table("computers_components", Schema = "Technic")]
public partial class ComputersComponent
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("computer_id")]
    public int ComputerId { get; set; }

    [Column("component_name")]
    [StringLength(255)]
    public string ComponentName { get; set; } = null!;

    [Column("component_invnum")]
    [StringLength(255)]
    public string? ComponentInvnum { get; set; }

    [Column("component_specifies")]
    [StringLength(255)]
    public string? ComponentSpecifies { get; set; }

    [ForeignKey("ComputerId")]
    [InverseProperty("ComputersComponents")]
    public virtual Computer Computer { get; set; } = null!;
}
