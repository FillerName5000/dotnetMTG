using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Howest.MagicCards.DAL.Models;

[Table("cards")]
public partial class Card
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Column("mana_cost")]
    [StringLength(255)]
    public string ManaCost { get; set; }

    [Required]
    [Column("converted_mana_cost")]
    [StringLength(255)]
    public string ConvertedManaCost { get; set; }

    [Required]
    [Column("type")]
    [StringLength(255)]
    public string Type { get; set; }

    [Column("rarity_code")]
    [StringLength(255)]
    public string RarityCode { get; set; }

    [Required]
    [Column("set_code")]
    [StringLength(255)]
    public string SetCode { get; set; }

    [Column("text")]
    public string Text { get; set; }

    [Column("flavor")]
    public string Flavor { get; set; }

    [Column("artist_id")]
    public long? ArtistId { get; set; }

    [Required]
    [Column("number")]
    [StringLength(255)]
    public string Number { get; set; }

    [Column("power")]
    [StringLength(255)]
    public string Power { get; set; }

    [Column("toughness")]
    [StringLength(255)]
    public string Toughness { get; set; }

    [Required]
    [Column("layout")]
    [StringLength(255)]
    public string Layout { get; set; }

    [Column("multiverse_id")]
    public int? MultiverseId { get; set; }

    [Column("original_image_url")]
    [StringLength(255)]
    public string OriginalImageUrl { get; set; }

    [Required]
    [Column("image")]
    [StringLength(255)]
    public string Image { get; set; }

    [Column("original_text")]
    public string OriginalText { get; set; }

    [Column("original_type")]
    [StringLength(255)]
    public string OriginalType { get; set; }

    [Required]
    [Column("mtg_id")]
    [StringLength(255)]
    public string MtgId { get; set; }

    [Column("variations")]
    public string Variations { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ArtistId")]
    [InverseProperty("Cards")]
    public virtual Artist Artist { get; set; }

    [InverseProperty("Card")]
    public virtual ICollection<CardColor> CardColors { get; set; } = new List<CardColor>(); 

    [InverseProperty("Card")]
    public virtual ICollection<CardType> CardTypes { get; set; } = new List<CardType>();

    [ForeignKey("RarityCode")]
    [InverseProperty("Cards")]
    public virtual Rarity RarityCodeNavigation { get; set; }

    [ForeignKey("SetCode")]
    [InverseProperty("Cards")]
    public virtual Set SetCodeNavigation { get; set; }
}
