using TpwManage.Core.Entities;

namespace TpwManage.Application.ClientService.Queries.GetClientById;

internal class ClientByIdResponseDto(Guid id, string name)
{
  public Guid Id { get; private set; } = id;
  public string Name { get; private set; } = name;

  public static ClientByIdResponseDto FromEntity(Client client)
    => new(client.Id, client.Name);
}
