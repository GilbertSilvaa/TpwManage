using MediatR;

namespace TpwManage.Application.ProductService.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;

