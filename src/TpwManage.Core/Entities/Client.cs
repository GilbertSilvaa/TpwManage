namespace TpwManage.Core.Entities;

public class Client(string name) : EntityBase
{
  public string Name { get; private set; } = name;
}
