namespace TpwManage.Core.Entities;

public abstract class EntityBase
{
  public EntityBase()
  {
    Id = Guid.NewGuid();
    CreateAt = DateTime.UtcNow;
  }

  public Guid Id { get; private set; }
  public DateTime CreateAt { get; private set; }
}
