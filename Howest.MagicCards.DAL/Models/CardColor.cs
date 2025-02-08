using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Models;

[PrimaryKey("CardId", "ColorId")]
[Table("card_colors")]
[Index("CardId", "ColorId", Name = "card_colors_card_id_color_id_unique", IsUnique = true)]
public partial class CardColor
{
    [Key]
    [Column("card_id")]
    public long CardId { get; set; }

    [Key]
    [Column("color_id")]
    public long ColorId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CardId")]
    [InverseProperty("CardColors")]
    public virtual Card Card { get; set; }

    [ForeignKey("ColorId")]
    [InverseProperty("CardColors")]
    public virtual Color Color { get; set; }
}
