using big_data.Models;

using Microsoft.EntityFrameworkCore;

public class BigDataContext : DbContext
{
    public BigDataContext(DbContextOptions<BigDataContext> options) : base(options) { }

    public DbSet<Contact> ContactsLOL { get; set; }
    public DbSet<Company> Companiezz { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Company - Contact relationship
        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Company)
            .WithMany(comp => comp.Contacts)
            .HasForeignKey(c => c.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);  // Ensures that deleting a company will delete its associated contacts

    }

}

