using Microsoft.EntityFrameworkCore;
using Exercise6.Models;

namespace Exercise6.Data;


public class WarehouseContext : DbContext
{
    public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProductWarehouse> ProductWarehouses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Warehouse>().ToTable("Warehouse");
        modelBuilder.Entity<Order>().ToTable("Order");
        modelBuilder.Entity<ProductWarehouse>().ToTable("ProductWarehouse");

        // Product Configuration
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("numeric(25, 2)");

        // ProductWarehouse Configuration
        modelBuilder.Entity<ProductWarehouse>()
            .Property(pw => pw.Price)
            .HasColumnType("numeric(25, 2)");

        // Relationships and Keys
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.IdProduct);

        modelBuilder.Entity<ProductWarehouse>()
            .HasOne(pw => pw.Product)
            .WithMany(p => p.ProductWarehouses)
            .HasForeignKey(pw => pw.IdProduct);

        modelBuilder.Entity<ProductWarehouse>()
            .HasOne(pw => pw.Warehouse)
            .WithMany(w => w.ProductWarehouses)
            .HasForeignKey(pw => pw.IdWarehouse);

        modelBuilder.Entity<ProductWarehouse>()
            .HasOne(pw => pw.Order)
            .WithMany(o => o.ProductWarehouses)
            .HasForeignKey(pw => pw.IdOrder);
        
        modelBuilder.Entity<Product>()
            .Property(p => p.IdProduct)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Warehouse>()
            .Property(w => w.IdWarehouse)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Order>()
            .Property(o => o.IdOrder)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<ProductWarehouse>()
            .Property(pw => pw.IdProductWarehouse)
            .ValueGeneratedOnAdd();
    }
}

