using GeneralStoreMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreMVC.Data;

public partial class GeneralStoreDbContext : DbContext
{
    public GeneralStoreDbContext()
    {
    }

    public GeneralStoreDbContext(DbContextOptions<GeneralStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerEntity> Customers { get; set; }

    public virtual DbSet<ProductEntity> Products { get; set; }

    public virtual DbSet<TransactionEntity> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07D8E9EEBB");

            entity.ToTable("Customers", "dev");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07E0C85961");

            entity.ToTable("Products", "dev");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transati__3214EC079ACC1559");

            entity.ToTable("Transactions", "dev");

            entity.Property(e => e.DateOfTransaction).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transatio__Custo__2B3F6F97");

            entity.HasOne(d => d.Product).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transatio__Produ__2A4B4B5E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
