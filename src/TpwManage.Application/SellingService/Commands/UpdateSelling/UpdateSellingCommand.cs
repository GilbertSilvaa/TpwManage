using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TpwManage.Application.SellingService.Commands.UpdateSelling;

public class UpdateSellingCommand : IRequest<SellingResponse>
{
  [Required(ErrorMessage = "Id é um campo obrigatório.")]
  public Guid Id { get; set; }

  [Required(ErrorMessage = "ProductsId é um campo obrigatório.")]
  public List<Guid> ProductsId { get; set; } = [];
}
