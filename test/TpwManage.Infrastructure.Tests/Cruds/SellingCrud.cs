using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests.Cruds;

public class SellingCrud(DbTest db) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = db.ServiceProvider;

  [Fact]
  public async Task IsPossible_CreateSelling()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    Selling selling = new()
    {
      Client = await GenerateClientAsync(new(context!))
    };
    selling.SetupProducts(await GenerateProductListAsync(new(context!)));
    Selling sellingCreated = await _sellingRepository.CreateAsync(selling);

    Assert.Equal(selling, sellingCreated);
  }
  
  [Fact]
  public async Task IsPossible_SelectAllSellings()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    await GetOneSellingAsync(context!);
    var allSellingsList = await _sellingRepository.GetAllAsync();

    Assert.True(allSellingsList.Count > 0);
  }

  [Fact]
  public async Task IsPossible_SelectSellingById()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    var selling = await GetOneSellingAsync(context!);
    var sellingSelected = await _sellingRepository.GetByIdAsync(selling.Id);

    Assert.Equal(selling.ToString(), sellingSelected?.ToString());
  }

  [Fact]
  public async Task IsPossible_UpdateSelling()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    var selling = await GetOneSellingAsync(context!);
    selling.Client = await GenerateClientAsync(new(context!));
    selling.ClearProducts();
    selling.SetupProducts(await GenerateProductListAsync(new(context!)));
    var sellingUpdated = await _sellingRepository.UpdateAsync(selling);

    Assert.Equal(selling, sellingUpdated);
  }

  [Fact]
  public async Task IsPossible_DeleteSelling()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    var selling = await GetOneSellingAsync(context!);
    var isDeleted = await _sellingRepository.DeleteAsync(selling.Id);

    Assert.True(isDeleted);
  }
  
  [Fact]
  public async Task IsPossible_SelectEmptySellingList()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    SellingRepository _sellingRepository = new(context!);

    await RemoveAllSellingsAsync(_sellingRepository);
    var emptyClientList = await _sellingRepository.GetAllAsync();

    Assert.Empty(emptyClientList);
  }

  private static async Task<Selling> GetOneSellingAsync(MyContext context)
  {
    SellingRepository repository = new(context!);
    var sellingList = await repository.GetAllAsync();

    if (sellingList.Count > 0) return sellingList.First();

    Selling selling = new()
    {
      Client = await GenerateClientAsync(new(context!))
    };
    selling.SetupProducts(await GenerateProductListAsync(new(context!)));

    return await repository.CreateAsync(selling);
  }

  private static async Task<List<Product>> GenerateProductListAsync(ProductRepository repository)
  {
    List<Product> products = [];
    Random random = new();

    foreach (var _ in Enumerable.Range(0, 8))
    {
      Product product = new(
        name: Faker.Name.First(),
        color: Faker.Name.Last(),
        price: random.Next(20, 80),
        amount: random.Next(5, 35));

      var productCreated = await repository.CreateAsync(product);
      products.Add(productCreated);
    }

    return products;
  }

  private static async Task<Client> GenerateClientAsync(ClientRepository repository)
    => await repository.CreateAsync(new(Faker.Name.FullName()));

  private static async Task RemoveAllSellingsAsync(SellingRepository repository)
  {
    foreach (var selling in (await repository.GetAllAsync()))
      await repository.DeleteAsync(selling.Id);
  }
}
