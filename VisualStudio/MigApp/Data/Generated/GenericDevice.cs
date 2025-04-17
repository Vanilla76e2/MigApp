using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Data;

[Table("generic_devices", Schema = "Technic")]
public partial class GenericDevice
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("device_type")]
    [StringLength(255)]
    public string DeviceType { get; set; } = null!;

    [Column("device_name")]
    [StringLength(255)]
    public string DeviceName { get; set; } = null!;

    [Column("device_invnum")]
    [StringLength(255)]
    public string? DeviceInvnum { get; set; }

    [Column("device_specification")]
    public string? DeviceSpecification { get; set; }

    [Column("device_comment")]
    public string? DeviceComment { get; set; }
}
