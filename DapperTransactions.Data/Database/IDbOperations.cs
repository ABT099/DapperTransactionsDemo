namespace DapperTransactions.Data.Database;

public interface IDbOperations : IDisposable
{
    // Add more methods as needed (extend from dapper)
    Task<IEnumerable<T>> QueryAsync<T>(string sql, params object[] parameters);
    Task<T?> QuerySingleOrDefaultAsync<T>(string sql, params object[] parameters);
    Task<T> QuerySingleAsync<T>(string sql, params object[] parameters);
    Task<int> ExecuteAsync(string sql, params object[] parameters);
}