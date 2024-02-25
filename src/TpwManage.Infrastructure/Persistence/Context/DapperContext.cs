using Dapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace TpwManage.Infrastructure.Persistence.Context;

public class DapperContext(MyContext context)
{
  private MySqlConnection GetConnection()
    => new(context.Database.GetConnectionString());

  public async Task<List<T>> ExecuteQueryAsync<T>(string SQL)
  {
    using var connection = GetConnection();
    connection.Open();

    return (await connection.QueryAsync<T>(SQL)).ToList();
  }
}
