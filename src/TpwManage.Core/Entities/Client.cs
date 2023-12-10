namespace TpwManage.Core.Entities;

public class Client(string name, Guid? id = null) : EntityBase(id)
{
  public string Name { get; private set; } = name;
}
