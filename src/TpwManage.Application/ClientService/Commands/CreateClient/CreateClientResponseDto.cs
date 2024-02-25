using TpwManage.Core.Entities;

namespace TpwManage.Application.ClientService.Commands.CreateClient;

internal class CreateClientResponseDto(Guid id, string name)
{
  public Guid Id { get; private set; } = id;
  public string Name { get; private set; } = name;

  public static CreateClientResponseDto FromEntity(Client client)
    => new(client.Id, client.Name);
}
