using System.ComponentModel.DataAnnotations;

namespace TpwManage.Application.InputModels;

public class CreateSellingInputModel
{
  [Required(ErrorMessage = "ClientId é um campo obrigatório.")]
  public Guid ClientId { get; set; }

  [Required(ErrorMessage = "ProductsId é um campo obrigatório.")]
  public List<Guid> ProductsId { get; set; } = [];
}

public class UpdateSellingInputModel
{
  [Required(ErrorMessage = "Id é um campo obrigatório.")]
  public Guid Id { get; set; }

  [Required(ErrorMessage = "ClientId é um campo obrigatório.")]
  public Guid ClientId { get; set; }

  [Required(ErrorMessage = "ProductsId é um campo obrigatório.")]
  public List<Guid> ProductsId { get; set; } = [];
}
