using MediatR;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.SellingService.Queries.GetSellingsByClientId;

internal class GetSellingsByClientIdQueryHandler(ISellingRepository repository) 
  : IRequestHandler<GetSellingsByClientIdQuery, List<SellingResponse>>
{
  private readonly ISellingRepository _repository = repository;

  public async Task<List<SellingResponse>> Handle(
    GetSellingsByClientIdQuery request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var response = await _repository.GetByClientIdAsync(request.ClientId);
      return [.. response.Select(SellingResponse.FromEntity)
        .OrderByDescending(s => s.DateSale)];
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
