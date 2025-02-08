using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Models;

[PrimaryKey("CardId", "TypeId")]
[Table("card_types")]
[Index("CardId", "TypeId", Name = "card_types_card_id_type_id_unique", IsUnique = true)]
public partial class CardType
{
    [Key]
    [Column("card_id")]
    public long CardId { get; set; }

    [Key]
    [Column("type_id")]
    public long TypeId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CardId")]
    [InverseProperty("CardTypes")]
    public virtual Card Card { get; set; }

    [ForeignKey("TypeId")]
    [InverseProperty("CardTypes")]
    public virtual Type Type { get; set; }
}
