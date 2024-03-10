using TpwManage.Core.Entities;

namespace TpwManage.Application;

internal class ClientResponse(Guid id, string name)
{
  public Guid Id { get; private set; } = id;
  public string Name { get; private set; } = name;

  public static ClientResponse FromEntity(Client client)
    => new(client.Id, client.Name);
}
