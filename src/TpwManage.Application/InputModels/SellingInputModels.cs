namespace TpwManage.Application.InputModels;

public class CreateSellingInputModel
{
  public Guid ClientId { get; set; }
  public List<Guid> ProductsId { get; set; } = [];
}

public class UpdateSellingInputModel
{
  public Guid Id { get; set; }
  public Guid ClientId { get; set; }
  public List<Guid> ProductsId { get; set; } = [];
}
