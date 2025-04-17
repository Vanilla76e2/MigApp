using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Data;

[Table("computers_service_history", Schema = "Technic")]
public partial class ComputersServiceHistory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("computer_id")]
    public int ComputerId { get; set; }

    [Column("service_date")]
    public DateOnly ServiceDate { get; set; }

    [Column("servicer")]
    [StringLength(255)]
    public string Servicer { get; set; } = null!;

    [Column("service_description")]
    public string ServiceDescription { get; set; } = null!;

    [ForeignKey("ComputerId")]
    [InverseProperty("ComputersServiceHistories")]
    public virtual Computer Computer { get; set; } = null!;
}
