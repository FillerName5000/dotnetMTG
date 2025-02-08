﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Models;

[Table("rarities")]
[Index("Code", Name = "rarities_code_unique", IsUnique = true)]
public partial class Rarity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("code")]
    [StringLength(255)]
    public string Code { get; set; }

    [Required]
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("RarityCodeNavigation")]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
