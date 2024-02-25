using MediatR;
using TpwManage.Core;
using TpwManage.Core.Entities;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ClientService.Commands.UpdateClient;

internal class UpdateClientCommandHandler(IClientRepository repository)
  : IRequestHandler<UpdateClientCommand, UpdateClientResponseDto>
{
  private readonly IClientRepository _repository = repository;

  public async Task<UpdateClientResponseDto> Handle(
    UpdateClientCommand request, 
    CancellationToken cancellationToken)
  {
    try
    {
      await Validation(request.ToEntity());

      var response = await _repository.UpdateAsync(request.ToEntity());

      return UpdateClientResponseDto.FromEntity(response!);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  private async Task Validation(Client client)
  {
    var response = await _repository.GetByIdAsync(client.Id)
      ?? throw new ClientNotFoundException();

    if (client.Name != response.Name && await _repository.ExistsAsync(response.Name))
      throw new InvalidOperationException("Já existe um cliente com esse nome.");
  }
}
