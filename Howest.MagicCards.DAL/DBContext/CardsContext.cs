using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.DBContext;

public partial class CardsContext : DbContext
{
    public CardsContext()
    {
    }

    public CardsContext(DbContextOptions<CardsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardColor> CardColors { get; set; }

    public virtual DbSet<CardType> CardTypes { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }

    public virtual DbSet<Rarity> Rarities { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<Models.Type> Types { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) // two options: put all validation in one context file, or in each of their respective models. Latter felt cleaner.
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__artists__3213E83FC867953C");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cards__3213E83F03749037");

            entity.HasOne(d => d.Artist).WithMany(p => p.Cards).HasConstraintName("FK_cards_artists");

            entity.HasOne(d => d.RarityCodeNavigation).WithMany(p => p.Cards)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.RarityCode)
                .HasConstraintName("FK_cards_rarities");

            entity.HasOne(d => d.SetCodeNavigation).WithMany(p => p.Cards)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.SetCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cards_sets");
        });

        modelBuilder.Entity<CardColor>(entity =>
        {
            entity.HasKey(e => new { e.CardId, e.ColorId }).HasName("card_colors_card_id_color_id_primary");

            entity.HasOne(d => d.Card).WithMany(p => p.CardColors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_card_colors_cards");

            entity.HasOne(d => d.Color).WithMany(p => p.CardColors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_card_colors_colors");
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.HasKey(e => new { e.CardId, e.TypeId }).HasName("card_types_card_id_type_id_primary");

            entity.HasOne(d => d.Card).WithMany(p => p.CardTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_card_types_cards");

            entity.HasOne(d => d.Type).WithMany(p => p.CardTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_card_types_types");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__colors__3213E83F2D3B99AB");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__migratio__3213E83FC12ED5C9");
        });

        modelBuilder.Entity<PersonalAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personal__3213E83FF1366E32");
        });

        modelBuilder.Entity<Rarity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rarities__3213E83F710C9509");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sets__3213E83F0F72A017");
        });

        modelBuilder.Entity<Models.Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__types__3213E83F62126D10");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
