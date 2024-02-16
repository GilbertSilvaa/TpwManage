using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests.Cruds;

public class ProductCrud(DbTest db) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = db.ServiceProvider;

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

    Assert.Equal(product, productCreated);
  }

  [Fact]
  public async Task IsPossible_SelectAllProducts()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    await GetOneProductAsync(_repository);
    var productList = await _repository.GetAllAsync();

    Assert.True(productList.Count > 0);
  }

  [Fact]
  public async Task IsPossible_SelectProductById()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    var product = await GetOneProductAsync(_repository);
    var productSelected = await _repository.GetByIdAsync(product.Id);

    Assert.Equal(product, productSelected);
  }

  [Fact]
  public async Task IsPossible_UpdateProduct()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    var product = await GetOneProductAsync(_repository);
    product.Name = Faker.Name.FullName();
    var productUpdated = await _repository.UpdateAsync(product);

    Assert.Equal(product, productUpdated);
  }

  [Fact]
  public async Task IsPossible_VerifyIfProductExist()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    var product = await GetOneProductAsync(_repository);
    bool productExist = await _repository.ExistsAsync(product.Name, product.Color);

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

    var product = await GetOneProductAsync(_repository);
    bool isDeleted = await _repository.DeleteAsync(product.Id);

    Assert.True(isDeleted);
  }

  [Fact]
  public async Task IsPossible_SelectEmptyProductList()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ProductRepository _repository = new(context!);

    await RemoveAllProductsAsync(_repository);
    var emptyProductList = await _repository.GetAllAsync();

    Assert.Empty(emptyProductList);
  }

  private static async Task<Product> GetOneProductAsync(ProductRepository repository)
  {
    var productList = await repository.GetAllAsync();

    if (productList.Count > 0) return productList.First();

    return await repository.CreateAsync(new(
      name: Faker.Name.First(),
      color: Faker.Name.Last(),
      price: Faker.RandomNumber.Next(),
      amount: Faker.RandomNumber.Next()));
  }

  private static async Task RemoveAllProductsAsync(ProductRepository repository)
  {
    foreach (var product in (await repository.GetAllAsync()))
      await repository.DeleteAsync(product.Id);
  }
}
