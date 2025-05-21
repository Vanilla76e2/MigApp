using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Infrastructure.Data.Entities;

[Table("logs", Schema = "Misc")]
public partial class LogEntry
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("action_date")]
    public DateTime ActionDate { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("Username")]
    [StringLength(255)]
    public string? Username { get; set; }

    [Column("user_ip")]
    [StringLength(15)]
    public string? UserIp { get; set; }

    [Column("source")]
    [StringLength(50)]
    public string? Source { get; set; }

    [Column("action_type")]
    [StringLength(100)]
    public string ActionType { get; set; } = null!;

    [Column("table_name")]
    [StringLength(100)]
    public string TableName { get; set; } = null!;

    [Column("record_id")]
    public int? RecordId { get; set; }

    [Column("changes")]
    [StringLength(1000)]
    public string? Changes { get; set; }

    [Column("specifies")]
    [StringLength(1000)]
    public string? Specifies { get; set; }
}
