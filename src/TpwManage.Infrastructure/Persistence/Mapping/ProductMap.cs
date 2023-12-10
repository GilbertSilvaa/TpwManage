using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TpwManage.Core.Entities;

namespace TpwManage.Infrastructure.Persistence.Mapping;

public class ProductMap : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable("Products");
    builder.HasKey(p => p.Id);
    builder.Property(p => p.Name).IsRequired();
    builder.Property(p => p.Price).IsRequired();
    builder.Property(p => p.Amount).IsRequired();
  }
}
