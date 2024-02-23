using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests.Cruds;

public class ProductCrud(DbTest db) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = db.ServiceProvider;
  private Product? _product;

  [Fact]
  public async Task IsPossible_CreateProduct()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    Product product = new(
      name: Faker.Name.First(),
      color: Faker.Name.Last(),
      price: Faker.RandomNumber.Next(),
      amount: Faker.RandomNumber.Next()); 

    Product productCreated = await _repository.CreateAsync(product);
    _product = productCreated;

    Assert.Equal(product.ToString(), productCreated.ToString());
  }

  [Fact]
  public async Task IsPossible_SelectAllProducts()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    _product ??= await CreateProductAsync(_repository);
    var productList = await _repository.GetAllAsync();

    Assert.True(productList.Count > 0);
  }

  [Fact]
  public async Task IsPossible_SelectProductById()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    _product ??= await CreateProductAsync(_repository);
    var productSelected = await _repository.GetByIdAsync(_product.Id);

    Assert.Equal(_product.ToString(), productSelected?.ToString());
  }

  [Fact]
  public async Task IsPossible_UpdateProduct()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    _product ??= await CreateProductAsync(_repository);
    _product.Name = Faker.Name.FullName();
    var productUpdated = await _repository.UpdateAsync(_product);

    Assert.Equal(_product.ToString(), productUpdated?.ToString());
  }

  [Fact]
  public async Task IsPossible_VerifyIfProductExist()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    _product ??= await CreateProductAsync(_repository);
    bool productExist = await _repository.ExistsAsync(_product.Name, _product.Color);

    Assert.True(productExist);
  }

  [Fact]
  public async Task IsPossible_VerifyIfProductNotExist()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    bool productExist = await _repository
      .ExistsAsync(Faker.Name.FullName(), Faker.Name.Last());

    Assert.False(productExist);
  }

  [Fact]
  public async Task IsPossible_DeleteProduct()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    _product ??= await CreateProductAsync(_repository);
    bool isDeleted = await _repository.DeleteAsync(_product.Id);

    Assert.True(isDeleted);
  }

  private static async Task<Product> CreateProductAsync(ProductRepository repository)
  {
    Random random = new();

    return await repository.CreateAsync(new(
      name: Faker.Name.First(),
      color: Faker.Name.Last(),
      price: random.Next(20, 80),
      amount: random.Next(5, 35)));
  }
}
