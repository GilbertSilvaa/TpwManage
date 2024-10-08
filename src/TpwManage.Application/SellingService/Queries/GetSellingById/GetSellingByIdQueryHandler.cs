﻿using MediatR;
using TpwManage.Core;
using TpwManage.Core.Repositories;

namespace TpwManage.Application.SellingService.Queries.GetSellingById;

internal class GetSellingByIdQueryHandler(ISellingRepository repository)
  : IRequestHandler<GetSellingByIdQuery, SellingResponse>
{
  private readonly ISellingRepository _repository = repository;

  public async Task<SellingResponse> Handle(
    GetSellingByIdQuery request, 
    CancellationToken cancellationToken)
  {
    try
    {
      var response = await _repository.GetByIdAsync(request.Id)
        ?? throw new SellingNotFoundException();

      return SellingResponse.FromEntity(response);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
}
