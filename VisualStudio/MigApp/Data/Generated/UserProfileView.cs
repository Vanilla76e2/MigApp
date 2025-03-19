using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MigApp.Data;

[Keyless]
public partial class UserProfileView
{
    [Column("id")]
    public int? Id { get; set; }

    [Column("username")]
    [StringLength(255)]
    public string? Username { get; set; }

    [Column("fio")]
    [StringLength(255)]
    public string? Fio { get; set; }

    [Column("password_status")]
    public string? PasswordStatus { get; set; }

    [Column("role_name")]
    [StringLength(255)]
    public string? RoleName { get; set; }
}
