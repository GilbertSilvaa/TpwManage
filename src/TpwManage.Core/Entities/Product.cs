namespace TpwManage.Core.Entities;

public class Product(string name, string color, float price, int amount) : EntityBase
{
  public string Name { get; private set; } = name;
  public string Color { get; private set; } = color;
  public float Price { get; private set; } = price;
  public int Amount { get; set; } = amount;
}
