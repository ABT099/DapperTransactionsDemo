using System.Data;

namespace DapperTransactions.Data.DbTransactions;

internal sealed class TransactionContext
{
    private static readonly AsyncLocal<IDbConnection?> CurrentConnection = new();
    private static readonly AsyncLocal<IDbTransaction?> CurrentTransaction = new();
    
    public static IDbConnection? Connection
    {
        get => CurrentConnection.Value;
        set => CurrentConnection.Value = value;
    }

    public static IDbTransaction? Transaction
    {
        get => CurrentTransaction.Value;
        set => CurrentTransaction.Value = value;
    }
}

