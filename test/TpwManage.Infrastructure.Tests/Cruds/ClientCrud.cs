using Microsoft.Extensions.DependencyInjection;
using TpwManage.Core.Entities;
using TpwManage.Infrastructure.Persistence.Context;
using TpwManage.Infrastructure.Persistence.Repositories;

namespace TpwManage.Infrastructure.Tests.Cruds;

public class ClientCrud(DbTest db) : TestBase, IClassFixture<DbTest>
{
  private readonly ServiceProvider _serviceProvider = db.ServiceProvider;
  private Client? _client;

  [Fact]
  public async Task IsPossible_CreateClient()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    Client client = new(Faker.Name.FullName());
    Client clientCreated = await _repository.CreateAsync(client);
    _client = clientCreated;

    Assert.Equal(client.ToString(), clientCreated.ToString());
  }

  [Fact]
  public async Task IsPossible_SelectAllClients()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    _client ??= await CreateClientAsync(_repository);
    var clientList = await _repository.GetAllAsync();

    Assert.True(clientList.Count > 0);
  }

  [Fact]
  public async Task IsPossible_SelectClientById()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    _client ??= await CreateClientAsync(_repository);
    var clientSelected = await _repository.GetByIdAsync(_client!.Id);

    Assert.Equal(_client.ToString(), clientSelected?.ToString());
  }

  [Fact]
  public async Task IsPossible_UpdateClient()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    _client ??= await CreateClientAsync(_repository);
    _client.Name = Faker.Name.FullName();
    var clientUpdated = await _repository.UpdateAsync(_client);

    Assert.Equal(_client.ToString(), clientUpdated?.ToString());
  }

  [Fact]
  public async Task IsPossible_VerifyIfClientExist()
  {
    using var context = _serviceProvider.GetService<MyContext>();
    ClientRepository _repository = new(context!);

    _client ??= await CreateClientAsync(_repository);
    bool clientExist = await _repository.ExistsAsync(_client.Name);

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

    _client ??= await CreateClientAsync(_repository);
    bool isDeleted = await _repository.DeleteAsync(_client.Id);

    Assert.True(isDeleted);
  }

  private static async Task<Client> CreateClientAsync(ClientRepository repository)
    => await repository.CreateAsync(new(Faker.Name.FullName()));
}