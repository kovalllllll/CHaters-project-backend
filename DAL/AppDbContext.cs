using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<ProductCharacteristic> ProductCharacteristics { get; set; }
}