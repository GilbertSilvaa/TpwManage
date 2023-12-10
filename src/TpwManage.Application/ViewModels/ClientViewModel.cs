using TpwManage.Core.Entities;

namespace TpwManage.Application.ViewModels;

public class ClientViewModel(Guid id, string name)
{
  public Guid Id { get; private set; } = id;
  public string Name { get; private set; } = name;

  public static ClientViewModel FromEntity(Client client) 
    => new(client.Id, client.Name);
}
