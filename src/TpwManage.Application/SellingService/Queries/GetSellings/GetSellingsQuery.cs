using MediatR;

namespace TpwManage.Application.SellingService.Queries.GetSellings;

public record GetSellingsQuery : IRequest<List<SellingResponse>>;

