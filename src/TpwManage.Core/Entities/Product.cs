namespace TpwManage.Core.Entities;

public class Product(string name, string color, float price, int amount) : EntityBase
{
  public string Name { get; set; } = name;
  public string Color { get; set; } = color;
  public float Price { get; set; } = price;
  public int Amount { get; set; } = amount;
}
