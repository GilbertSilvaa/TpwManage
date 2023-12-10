using Microsoft.EntityFrameworkCore;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Mapping;

namespace TpwManage.Infrastructure.Persistence.Context;

public class MyContext(DbContextOptions<MyContext> options) : DbContext(options)
{
  public DbSet<Client> Clients { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Selling> Sellings { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Client>(new ClientMap().Configure);
    modelBuilder.Entity<Product>(new ProductMap().Configure);
    modelBuilder.Entity<Selling>(new SellingMap().Configure);
  }
}
