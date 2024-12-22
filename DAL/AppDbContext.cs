using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}