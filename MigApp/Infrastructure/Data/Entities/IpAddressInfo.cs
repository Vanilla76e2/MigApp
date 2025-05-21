using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Infrastructure.Data.Entities;

[Table("ip_address_info", Schema = "Misc")]
public partial class IpAddressInfo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ipaddress")]
    public long Ipaddress { get; set; }

    [Column("device")]
    [StringLength(255)]
    public string? Device { get; set; }

    [Column("inventorynumber")]
    [StringLength(255)]
    public string? Inventorynumber { get; set; }

    [Column("devicename")]
    [StringLength(255)]
    public string? Devicename { get; set; }

    [Column("comment")]
    public string? Comment { get; set; }
}
