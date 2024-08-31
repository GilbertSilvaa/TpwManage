using MediatR;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.SellingService.Queries.GetSellings;

internal class GetSellingsQueryHandler(ISellingRepository repository)
  : IRequestHandler<GetSellingsQuery, List<SellingResponse>>
{
  private readonly ISellingRepository _repository = repository;

  public async Task<List<SellingResponse>> Handle(
    GetSellingsQuery request,
    CancellationToken cancellationToken)
  {
    try
    {
      var response = await _repository.GetAllAsync();
      return [.. response.Select(SellingResponse.FromEntity)
      .OrderByDescending(s => s.DateSale)];
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
