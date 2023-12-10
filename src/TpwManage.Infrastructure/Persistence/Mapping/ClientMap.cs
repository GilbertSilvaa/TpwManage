using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TpwManage.Core.Entities;

namespace TpwManage.Infrastructure.Persistence.Mapping;

public class ClientMap : IEntityTypeConfiguration<Client>
{
  public void Configure(EntityTypeBuilder<Client> builder)
  {
    builder.ToTable("Clients");
    builder.HasKey(c => c.Id);
    builder.HasIndex(c => c.Name).IsUnique();
    builder.Property(c => c.Name).IsRequired();
  }
}
