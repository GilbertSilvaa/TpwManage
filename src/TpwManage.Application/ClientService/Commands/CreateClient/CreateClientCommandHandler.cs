﻿using MediatR;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ClientService.Commands.CreateClient;

internal class CreateClientCommandHandler(IClientRepository repository) 
  : IRequestHandler<CreateClientCommand, ClientResponse>
{
  private readonly IClientRepository _repository = repository;

  public async Task<ClientResponse> Handle(
    CreateClientCommand request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var client = request.ToEntity();

      if (await _repository.ExistsAsync(client.Name))
        throw new InvalidOperationException("Já existe um cliente com esse nome.");

      var response = await _repository.CreateAsync(client);
      return ClientResponse.FromEntity(response);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
