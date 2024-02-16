using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests.Cruds;

public class ClientCrud(DbTest db) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = db.ServiceProvider;

  [Fact]
  public async Task IsPossible_CreateClient()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    Client client = new(Faker.Name.FullName());
    Client clientCreated = await _repository.CreateAsync(client);

    Assert.Equal(client, clientCreated);
  }

  [Fact]
  public async Task IsPossible_SelectAllClients()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    await GetOneClientAsync(_repository);
    var clientList = await _repository.GetAllAsync();

    Assert.True(clientList.Count > 0);
  }

  [Fact]
  public async Task IsPossible_SelectClientById()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    var client = await GetOneClientAsync(_repository);
    var clientSelected = await _repository.GetByIdAsync(client.Id);

    Assert.Equal(client, clientSelected);
  }

  [Fact]
  public async Task IsPossible_UpdateClient()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    var client = await GetOneClientAsync(_repository);
    client.Name = Faker.Name.FullName();
    var clientUpdated = await _repository.UpdateAsync(client);

    Assert.Equal(client, clientUpdated);
  }

  [Fact]
  public async Task IsPossible_VerifyIfClientExist()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    var client = await GetOneClientAsync(_repository);
    bool clientExist = await _repository.ExistsAsync(client.Name);

    Assert.True(clientExist);
  }

  [Fact]
  public async Task IsPossible_VerifyIfClientNotExist()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    bool clientExist = await _repository.ExistsAsync(Faker.Name.FullName());

    Assert.False(clientExist);
  }

  [Fact]
  public async Task IsPossible_DeleteClient()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    var client = await GetOneClientAsync(_repository);
    bool isDeleted = await _repository.DeleteAsync(client.Id);

    Assert.True(isDeleted);
  }

  [Fact]
  public async Task IsPossible_SelectEmptyClientList()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    await RemoveAllClientsAsync(_repository);
    var emptyClientList = await _repository.GetAllAsync();

    Assert.Empty(emptyClientList);
  }

  private static async Task<Client> GetOneClientAsync(ClientRepository repository)
  {
    var clientList = await repository.GetAllAsync();

    if (clientList.Count > 0) return clientList.First();

    return await repository.CreateAsync(new(Faker.Name.FullName()));
  }

  private static async Task RemoveAllClientsAsync(ClientRepository repository)
  {
    foreach (var client in (await repository.GetAllAsync()))
      await repository.DeleteAsync(client.Id);
  }
}