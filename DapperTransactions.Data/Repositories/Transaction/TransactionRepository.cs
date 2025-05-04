using DapperTransactions.Contracts.Data;
using DapperTransactions.Data.Database;

namespace DapperTransactions.Data.Repositories.Transaction;

internal sealed class TransactionRepository(IDbOperations db) : ITransactionRepository
{
    public async Task<bool> Add(TransactionDto transaction)
    {
        var result = await db.ExecuteAsync(
        """
        INSERT INTO Transactions (Id, AccountId, Amount, Description, TransactionDate, Type)
        VALUES (@Id, @AccountId, @Amount, @Description, @TransactionDate, @Type)
        """, transaction);
        return result > 0;
    }

    public Task<IEnumerable<TransactionDto>> FindAll()
    {
        return db.QueryAsync<TransactionDto>("SELECT * FROM Transactions");
    }

    public Task<TransactionDto?> FindById(Guid id)
    {
        return db.QuerySingleOrDefaultAsync<TransactionDto>("SELECT * FROM Transactions WHERE Id = @Id", new {Id = id});
    }

    public async Task<bool> Update(TransactionDto transaction)
    {
        var result = await db.ExecuteAsync(
        """
        UPDATE Transactions
        SET AccountId = @AccountId, 
            Amount = @Amount, 
            Description = @Description, 
            TransactionDate = @TransactionDate, 
            Type = @Type
        WHERE Id = @Id
        """, transaction);
        return result > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var result = await db.ExecuteAsync("DELETE FROM Transactions WHERE Id = @Id", new { Id = id });
        return result > 0;
    }
}