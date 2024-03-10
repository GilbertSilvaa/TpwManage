using MediatR;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ClientService.Queries.GetClients;

internal class GetClientsQueryHandler(IClientRepository repository) 
  : IRequestHandler<GetClientsQuery, List<ClientResponse>>
{
  private readonly IClientRepository _repository = repository;

  public async Task<List<ClientResponse>> Handle(
    GetClientsQuery request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var response = await _repository.GetAllAsync();
      return [.. response.Select(ClientResponse.FromEntity).OrderBy(c => c.Name)];
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
