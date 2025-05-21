using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigApp.Infrastructure.Data.Entities;

[PrimaryKey("UserId", "TableName", "RowId")]
[Table("favourite", Schema = "Misc")]
public partial class Favourite
{
    [Column("faved_date")]
    public DateTime FavedDate { get; set; }

    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Key]
    [Column("table_name")]
    [StringLength(100)]
    public string TableName { get; set; } = null!;

    [Key]
    [Column("row_id")]
    public int RowId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Favourites")]
    public virtual UsersProfile User { get; set; } = null!;
}
