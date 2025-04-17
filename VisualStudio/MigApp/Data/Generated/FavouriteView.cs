using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Data;

[Keyless]
public partial class FavouriteView
{
    [Column("faved_date")]
    public DateTime? FavedDate { get; set; }

    [Column("table_name")]
    [StringLength(100)]
    public string? TableName { get; set; }

    [Column("item_description", TypeName = "character varying")]
    public string? ItemDescription { get; set; }

    [Column("comment", TypeName = "character varying")]
    public string? Comment { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }
}
