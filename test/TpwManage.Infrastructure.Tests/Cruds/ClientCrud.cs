using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests.Cruds;

public class ClientCrud(DbTest db) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = db.ServiceProvider;

  [Fact]
  public async Task IsPossibleCreateClient()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    Client client = new(Faker.Name.FullName());
    Client clientCreated = await _repository.CreateAsync(client);

    Assert.NotNull(clientCreated);
    Assert.Equal(client.Id, clientCreated.Id);
    Assert.Equal(client.Name, clientCreated.Name);
    Assert.False(clientCreated.Id == Guid.Empty);
  }

  [Fact]
  public async Task IsPossibleSelectAllClients()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    await _repository.CreateAsync(new(Faker.Name.FullName()));
    var clientList = await _repository.GetAllAsync();

    Assert.NotNull(clientList);
    Assert.True(clientList.Count > 0);
  }

  [Fact]
  public async Task IsPossibleSelectClientById()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    var client = await _repository.CreateAsync(new(Faker.Name.FullName()));
    var clientSelected = await _repository.GetByIdAsync(client.Id);

    Assert.NotNull(clientSelected); 
    Assert.Equal(client.Name, clientSelected.Name);
    Assert.Equal(client.Id, clientSelected.Id);
  }

  [Fact]
  public async Task IsPossibleUpdateClient()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    var client = await _repository.CreateAsync(new(Faker.Name.FullName()));
    client.Name = Faker.Name.FullName();
    var clientUpdated = await _repository.UpdateAsync(client);

    Assert.NotNull(clientUpdated);
    Assert.Equal(client.Name, clientUpdated.Name);
    Assert.Equal(client.Id, clientUpdated.Id);
  }

  [Fact]
  public async Task IsPossibleVerifyIfClientExist()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    var client = await _repository.CreateAsync(new(Faker.Name.FullName()));
    bool clientExist = await _repository.ExistsAsync(client.Name);

    Assert.True(clientExist);
  }

  [Fact]
  public async Task IsPossibleVerifyIfClientNotExist()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    bool clientExist = await _repository.ExistsAsync(Faker.Name.FullName());

    Assert.False(clientExist);
  }

  [Fact]
  public async Task IsPossibleDeleteClient()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    var client = await _repository.CreateAsync(new(Faker.Name.FullName()));
    bool isDeleted = await _repository.DeleteAsync(client.Id);

    Assert.True(isDeleted);
  }

  [Fact]
  public async Task IsPossibleSelectEmptyClientList()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    var clientList = await _repository.GetAllAsync();

    foreach (var client in clientList)
      await _repository.DeleteAsync(client.Id);

    var emptyClientList = await _repository.GetAllAsync();

    Assert.NotNull(emptyClientList);
    Assert.Empty(emptyClientList);
  }
}