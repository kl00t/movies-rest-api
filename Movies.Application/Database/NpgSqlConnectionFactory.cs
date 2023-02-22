using Npgsql;
using System.Data;

namespace Movies.Application.Database;

public class NpgSqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public NpgSqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken: token);
        return connection;
    }
}