using MediatR;
using TpwManage.Core;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ClientService.Queries.GetClientById;

internal class GetClientByIdQueryHandler(IClientRepository repository)
  : IRequestHandler<GetClientByIdQuery, ClientResponse>
{
  private readonly IClientRepository _repository = repository;

  public async Task<ClientResponse> Handle(
    GetClientByIdQuery request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var response = await _repository.GetByIdAsync(request.Id)
        ?? throw new ClientNotFoundException();

      return ClientResponse.FromEntity(response);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
