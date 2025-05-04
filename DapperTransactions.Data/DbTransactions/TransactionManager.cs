using System.Data;
using DapperTransactions.Data.Database;

namespace DapperTransactions.Data.DbTransactions;

public interface ITransactionManager : IDisposable
{
    void StartTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}

internal sealed class TransactionManager(IDbConnectionFactory connectionFactory) : ITransactionManager
{
    private IDbConnection? _connection;
    private IDbTransaction? _transaction;
    
    public void StartTransaction()
    {
        _connection ??= connectionFactory.CreateConnectionAsync().GetAwaiter().GetResult();
        _transaction = _connection.BeginTransaction();

        TransactionContext.Connection = _connection;
        TransactionContext.Transaction = _transaction;
    }

    public void CommitTransaction()
    {
        _transaction?.Commit();
        ResetTransaction();
    }

    public void RollbackTransaction()
    {
        _transaction?.Rollback();
        ResetTransaction();
    }

    private void ResetTransaction()
    {
        _transaction = null;
        TransactionContext.Transaction = null;
    }

    
    public void Dispose()
    {
        _connection?.Dispose();
        _transaction?.Dispose();
        
        TransactionContext.Connection = null;
        TransactionContext.Transaction = null;
    }
}