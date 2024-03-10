using TpwManage.Core.Entities;

namespace TpwManage.Application.ClientService.Commands.UpdateClient;

internal class UpdateClientResponse(Guid id, string name)
{
  public Guid Id { get; private set; } = id;
  public string Name { get; private set; } = name;

  public static UpdateClientResponse FromEntity(Client client)
    => new(client.Id, client.Name);
}
