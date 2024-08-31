using MediatR;

namespace TpwManage.Application.SellingService.Commands.DeleteSelling;

public record DeleteSellingCommand(Guid Id) : IRequest<bool>;

