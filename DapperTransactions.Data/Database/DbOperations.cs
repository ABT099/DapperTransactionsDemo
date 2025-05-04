using System.Data;
using Dapper;
using DapperTransactions.Data.DbTransactions;

namespace DapperTransactions.Data.Database;

internal sealed class DbOperationManager : IDbOperations
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction? _transaction;
    
    public DbOperationManager(IDbConnectionFactory connectionFactory)
    {
        if (TransactionContext.Connection is null)
        {
            _connection = connectionFactory.CreateConnectionAsync().GetAwaiter().GetResult();
        }
        else
        {
            _connection = TransactionContext.Connection;
            _transaction = TransactionContext.Transaction;
        }
    }
    
    public async Task<int> ExecuteAsync(string sql, params object[] parameters) =>
        await _connection.ExecuteAsync(sql, parameters, _transaction);
    
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, params object[] parameters) =>
        await _connection.QueryAsync<T>(sql, parameters, _transaction);
    
    public async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, params object[] parameters) =>
        await _connection.QuerySingleOrDefaultAsync<T>(sql, parameters, _transaction);
    
    public async Task<T> QuerySingleAsync<T>(string sql, params object[] parameters) =>
        await _connection.QuerySingleAsync<T>(sql, parameters, _transaction);
    
    
    public void Dispose()
    {
        if (_transaction is not null) 
            return;
        
        _connection.Dispose();
    }
}