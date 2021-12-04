using Microsoft.EntityFrameworkCore;

namespace TestWithLocaldb.DataAccess;

public class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public virtual DbSet<Product> Products => this.Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=sample-database;Integrated Security=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Japanese_CI_AS");

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128);

            entity.Property(e => e.Publisher)
                .IsRequired()
                .HasMaxLength(128);
        });
    }
}
