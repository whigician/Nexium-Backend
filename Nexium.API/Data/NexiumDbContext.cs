﻿using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data;

public class NexiumDbContext(DbContextOptions<NexiumDbContext> options) : DbContext(options)
{
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
    public DbSet<BusinessStatusTranslation> BusinessStatusesTranslations { get; set; }
    public DbSet<TargetMarketTranslation> TargetMarketsTranslations { get; set; }
    public DbSet<CurrencyTranslation> CurrenciesTranslations { get; set; }
    public DbSet<AddressTypeTranslation> AddressTypesTranslations { get; set; }
    public DbSet<ContactTypeTranslation> ContactTypesTranslations { get; set; }
    public DbSet<IdentifierTypeTranslation> IdentifierTypesTranslations { get; set; }

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
        modelBuilder.Entity<BusinessStatusTranslation>()
            .HasIndex(t => new { t.BusinessStatusId, t.LanguageCode })
            .IsUnique();
        modelBuilder.Entity<TargetMarketTranslation>()
            .HasIndex(t => new { t.TargetMarketId, t.LanguageCode })
            .IsUnique();
        modelBuilder.Entity<CurrencyTranslation>()
            .HasIndex(t => new { t.CurrencyId, t.LanguageCode })
            .IsUnique();
        modelBuilder.Entity<AddressTypeTranslation>()
            .HasIndex(t => new { t.AddressTypeId, t.LanguageCode })
            .IsUnique();
        modelBuilder.Entity<ContactTypeTranslation>()
            .HasIndex(t => new { t.ContactTypeId, t.LanguageCode })
            .IsUnique();
        modelBuilder.Entity<IdentifierTypeTranslation>()
            .HasIndex(t => new { t.IdentifierTypeId, t.LanguageCode })
            .IsUnique();
        modelBuilder.Entity<Language>().HasData(
            new Language { Code = "ar", Name = "العربية" },
            new Language { Code = "fr", Name = "Français" },
            new Language { Code = "en", Name = "English" }
        );
    }
}