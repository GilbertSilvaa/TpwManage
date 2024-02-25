using MediatR;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.ClientService.Queries.GetClients;

internal class GetClientsQueryHandler(IClientRepository repository) 
  : IRequestHandler<GetClientsQuery, List<ClientResponseDto>>
{
  private readonly IClientRepository _repository = repository;

  public async Task<List<ClientResponseDto>> Handle(
    GetClientsQuery request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var response = await _repository.GetAllAsync();
      return [.. response.Select(ClientResponseDto.FromEntity).OrderBy(c => c.Name)];
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
