using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Models;

[Table("types")]
public partial class Type
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    [Column("type")]
    [StringLength(255)]
    public string Type1 { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Type")]
    public virtual ICollection<CardType> CardTypes { get; set; } = new List<CardType>();
}
