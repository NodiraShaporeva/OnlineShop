using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Account> Accounts => Set<Account>();

    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}