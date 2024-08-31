using MediatR;
using System.ComponentModel.DataAnnotations;
using TpwManage.Core.Entities;

namespace TpwManage.Application.ProductService.Commands.CreateProduct;

public class CreateProductCommand : IRequest<ProductResponse>
{
  [Required(ErrorMessage = "Nome é um campo obrigatório.")]
  [StringLength(250, ErrorMessage = "O nome pode ter no maximo {1} caracteres.")]
  public string Name { get; set; } = string.Empty;

  [StringLength(50, ErrorMessage = "A cor pode ter no maximo {1} caracteres.")]
  public string Color { get; set; } = string.Empty;

  [Required(ErrorMessage = "Preço é um campo obrigatório.")]
  public float Price { get; set; }

  [Required(ErrorMessage = "Quantidade é um campo obrigatório.")]
  public int Amount { get; set; }

  public Product ToEntity() => new(Name, Color, Price, Amount);
}
