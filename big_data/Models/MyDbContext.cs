using big_data.Models;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    public DbSet<Facebook> Facebooks { get; set; }
    public DbSet<Instagram> Instagrams { get; set; }
    public DbSet<LinkedIn> LinkedIns { get; set; }
    public DbSet<OtherContact> OtherContacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Email>()
            .HasOne(e => e.Company)
            .WithMany(c => c.Emails)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Facebook>()
            .HasOne(f => f.Company)
            .WithMany(c => c.Facebooks)
            .HasForeignKey(f => f.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Instagram>()
            .HasOne(i => i.Company)
            .WithMany(c => c.Instagrams)
            .HasForeignKey(i => i.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LinkedIn>()
            .HasOne(l => l.Company)
            .WithMany(c => c.LinkedIns)
            .HasForeignKey(l => l.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OtherContact>()
            .HasOne(o => o.Company)
            .WithMany(c => c.OtherContacts)
            .HasForeignKey(o => o.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PhoneNumber>()
            .HasOne(p => p.Company)
            .WithMany(c => c.PhoneNumbers)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}

