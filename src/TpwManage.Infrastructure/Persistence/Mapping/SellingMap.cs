using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TpwManage.Core.Entities;

namespace TpwManage.Infrastructure.Persistence.Mapping;

public class SellingMap : IEntityTypeConfiguration<Selling>
{
  public void Configure(EntityTypeBuilder<Selling> builder)
  {
    builder.ToTable("Sellings");
    builder.HasKey(s => s.Id);
  }
}
