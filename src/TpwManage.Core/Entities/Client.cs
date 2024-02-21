namespace TpwManage.Core.Entities;

public class Client : EntityBase
{
  public Client() => Name = string.Empty;

  public Client(string name) => Name = name;

  public string Name { get; set; }
}
