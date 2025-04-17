using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Data;

[Table("computers_devices", Schema = "Technic")]
public partial class ComputersDevice
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("computer_id")]
    public int ComputerId { get; set; }

    [Column("device_type")]
    [StringLength(255)]
    public string DeviceType { get; set; } = null!;

    [Column("device_id")]
    public int DeviceId { get; set; }

    [Column("device_name")]
    [StringLength(255)]
    public string? DeviceName { get; set; }

    [Column("device_invnum")]
    [StringLength(255)]
    public string? DeviceInvnum { get; set; }

    [Column("device_specification")]
    public string? DeviceSpecification { get; set; }

    [Column("device_comment")]
    public string? DeviceComment { get; set; }

    [ForeignKey("ComputerId")]
    [InverseProperty("ComputersDevices")]
    public virtual Computer Computer { get; set; } = null!;
}
