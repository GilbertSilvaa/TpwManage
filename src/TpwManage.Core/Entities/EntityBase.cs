namespace TpwManage.Core.Entities;

public abstract class EntityBase()
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public DateTime CreateAt { get; set; } = DateTime.UtcNow;
}
