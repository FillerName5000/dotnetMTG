using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Models;

[Table("artists")]
public partial class Artist
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("full_name")]
    [StringLength(255)]
    public string FullName { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Artist")]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
