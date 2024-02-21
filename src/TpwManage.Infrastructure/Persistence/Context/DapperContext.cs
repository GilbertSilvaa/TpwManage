using Dapper;
using MySqlConnector;

namespace TpwManage.Infrastructure.Persistence.Context;

public class DapperContext
{
  private static MySqlConnection GetConnection()
    => new(Environment.GetEnvironmentVariable("CONNECTION"));

  public static async Task<List<T>> ExecuteQueryAsync<T>(string SQL)
  {
    using var connection = GetConnection();
    connection.Open();

    return (await connection.QueryAsync<T>(SQL)).ToList();
  }
}
