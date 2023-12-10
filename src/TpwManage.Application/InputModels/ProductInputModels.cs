using TpwManage.Core.Entities;

namespace TpwManage.Application.InputModels;

public class CreateProductInputModel
{
  public string Name { get; set; } = string.Empty;
  public string Color { get; set; } = string.Empty;
  public float Price { get; set; }
  public int Amount { get; set; }

  public Product ToEntity() => new(Name, Color, Price, Amount);
}

public class UpdateProductInputModel
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Color { get; set; } = string.Empty;
  public float Price { get; set; }
  public int Amount { get; set; }

  public Product ToEntity() => new(Name, Color, Price, Amount){ Id = Id };
}
