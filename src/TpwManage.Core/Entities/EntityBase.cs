namespace TpwManage.Core.Entities;

public abstract class EntityBase(Guid? id = null)
{
  public Guid Id { get; set; } = id ?? Guid.NewGuid();
  public DateTime CreateAt { get; set; } = DateTime.UtcNow;
}
