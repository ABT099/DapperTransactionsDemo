using DapperTransactions.Contracts.Data;
using DapperTransactions.Data.Database;
using DapperTransactions.Domain;

namespace DapperTransactions.Data.Repositories.Account;

internal sealed class AccountRepository(IDbOperations db) : IAccountRepository
{
    public async Task<bool> Add(AccountDto account)
    {
        var result = await db.ExecuteAsync(
        """
        INSERT INTO Accounts (Id, CustomerId, AccountNumber, Balance, Type)
        VALUES (@Id, @CustomerId, @AccountNumber, @Balance, @Type)
        """, account);
        return result > 0;
    }

    public Task<IEnumerable<AccountDto>> FindAll()
    {
        return db.QueryAsync<AccountDto>("SELECT * FROM Accounts");
    }

    public Task<AccountDto?> FindById(Guid id)
    {
        return db.QuerySingleOrDefaultAsync<AccountDto>("SELECT * FROM Accounts WHERE Id = @Id", new {Id = id});
    }

    public Task<IEnumerable<AccountDto>> FindAllByType(AccountType type)
    {
        return db.QueryAsync<AccountDto>("SELECT * FROM Accounts WHERE Type = @Type", new {Type = (int) type});
    }

    public async Task<bool> Update(AccountDto account)
    {
        var result = await db.ExecuteAsync(
        """
        UPDATE Accounts
        SET CustomerId = @CustomerId, AccountNumber = @AccountNumber, Balance = @Balance, Type = @Type
        WHERE Id = @Id
        """, account);
        return result > 0;
    }

    public async Task<bool> UpdateBalance(Guid id, decimal amount)
    {
        var result = await db.ExecuteAsync(
        """
        UPDATE Accounts
        SET Balance = @Amount
        WHERE Id = @Id
        """, new { Id = id, Amount = amount });
        return result > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var result = await db.ExecuteAsync("DELETE FROM Accounts WHERE Id = @Id", new { Id = id });
        return result > 0;
    }
}