namespace TpwManage.Core.Entities;

public class Client : EntityBase
{
  public Client() => Name = string.Empty;

  public Client(string name) => Name = name;

  public string Name { get; set; }

  public override string ToString()
  {
    return $@"
      Id: {Id};
      Name: {Name}; 
      CreateAt: {CreateAt:mm:HH dd/MM/yyyy};
    ";
  }
}
