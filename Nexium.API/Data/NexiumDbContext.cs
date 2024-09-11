using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data;

public class NexiumDbContext : DbContext
{
    public NexiumDbContext(DbContextOptions<NexiumDbContext> options)
        : base(options)
    {
    }

    public DbSet<Industry> Industries { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<TargetMarket> TargetMarkets { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<ContactType> ContactTypes { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<IdentifierType> IdentifierTypes { get; set; }
    public DbSet<BusinessStatus> BusinessStatuses { get; set; }
    public DbSet<BusinessType> BusinessTypes { get; set; }
    public DbSet<IndustryTranslation> IndustryTranslations { get; set; }
    public DbSet<BusinessTypeTranslation> BusinessTypesTranslations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BusinessRelationship>()
            .HasKey(br => new { br.BusinessId, br.RelatedBusinessId });
        modelBuilder.Entity<BusinessRelationship>()
            .HasOne(br => br.Business)
            .WithMany(b => b.Suppliers)
            .HasForeignKey(br => br.BusinessId);
        modelBuilder.Entity<BusinessRelationship>()
            .HasOne(br => br.RelatedBusiness)
            .WithMany(b => b.Retailers)
            .HasForeignKey(br => br.RelatedBusinessId);
        modelBuilder.Entity<IndustryTranslation>()
            .HasIndex(t => new { t.IndustryId, t.LanguageCode })
            .IsUnique();
        modelBuilder.Entity<BusinessTypeTranslation>()
            .HasIndex(t => new { t.BusinessTypeId, t.LanguageCode })
            .IsUnique();
    }
}