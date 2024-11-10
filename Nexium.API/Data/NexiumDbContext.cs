using Microsoft.EntityFrameworkCore;
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
    public DbSet<TranslationMapping> TranslationMappings { get; set; }
    public DbSet<AppSection> AppSections { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<BusinessAddress> BusinessAddresses { get; set; }
    public DbSet<BusinessContact> BusinessContacts { get; set; }
    public DbSet<BusinessIdentifier> BusinessIdentifiers { get; set; }
    public DbSet<BusinessLink> BusinessLinks { get; set; }
    public DbSet<BusinessLinkType> BusinessLinkTypes { get; set; }
    public DbSet<BusinessMember> BusinessMembers { get; set; }
    public DbSet<BusinessOwner> BusinessOwners { get; set; }
    public DbSet<BusinessRelationship> BusinessRelationships { get; set; }
    public DbSet<BusinessRole> BusinessRoles { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<EmployeePosition> EmployeePositions { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PersonIdentifier> PersonIdentifiers { get; set; }
    public DbSet<PersonIdentifierType> PersonIdentifierTypes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<UserAccountActivity> UserAccountActivities { get; set; }
    public DbSet<UserAccountPermission> UserAccountPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BusinessRelationship>()
            .HasKey(br => new { br.SupplierId, br.RetailerId, br.BusinessRelationshipType });
        modelBuilder.Entity<BusinessRelationship>()
            .HasOne(br => br.Supplier)
            .WithMany(b => b.Retailers)
            .HasForeignKey(br => br.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<BusinessRelationship>()
            .HasOne(br => br.Retailer)
            .WithMany(b => b.Suppliers)
            .HasForeignKey(br => br.RetailerId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<TranslationMapping>()
            .HasIndex(t => new { t.EntityId, t.EntityTypeName, t.AttributeName, t.LanguageCode })
            .IsUnique();
        modelBuilder.Entity<Employee>()
            .HasOne(o => o.BusinessMember)
            .WithOne(e => e.Employee)
            .HasForeignKey<Employee>(e => e.BusinessMemberId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<BusinessOwner>()
            .HasOne(o => o.BusinessMember)
            .WithOne(e => e.BusinessOwner)
            .HasForeignKey<BusinessOwner>(e => e.BusinessMemberId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<UserAccount>()
            .HasOne(o => o.Employee)
            .WithOne(e => e.UserAccount)
            .HasForeignKey<UserAccount>(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<UserAccount>()
            .HasOne(o => o.BusinessOwner)
            .WithOne(e => e.UserAccount)
            .HasForeignKey<UserAccount>(e => e.BusinessOwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Language>().HasData(
            new Language { Code = "ar", Name = "العربية" },
            new Language { Code = "fr", Name = "Français" },
            new Language { Code = "en", Name = "English" }
        );
    }
}