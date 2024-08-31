using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TpwManage.Application.SellingService.Commands.CreateSelling;

public class CreateSellingCommand : IRequest<SellingResponse>
{
  [Required(ErrorMessage = "ClientId é um campo obrigatório.")]
  public Guid ClientId { get; set; }

  [Required(ErrorMessage = "ProductsId é um campo obrigatório.")]
  public List<Guid> ProductsId { get; set; } = [];
}
