using TpwManage.Core.Entities;

namespace TpwManage.Application.ClientService.Queries.GetClients;

internal class ClientResponseDto(Guid id, string name)
{
  public Guid Id { get; private set; } = id;
  public string Name { get; private set; } = name;

  public static ClientResponseDto FromEntity(Client client)
    => new(client.Id, client.Name);
}
