using DapperTransactions.Contracts.Data;
using DapperTransactions.Data.Database;

namespace DapperTransactions.Data.Repositories.Customer;

internal sealed class CustomerRepository(IDbOperations db) : ICustomerRepository
{
    public async Task<bool> Add(CustomerDto customer)
    {
        var result = await db.ExecuteAsync(
        """
        INSERT INTO Customers (Id, Name, Email)
        VALUES (@Id, @Name, @Email)
        """, customer);
        return result > 0;
    }

    public Task<IEnumerable<CustomerDto>> FindAll()
    {
        return db.QueryAsync<CustomerDto>("SELECT * FROM Customers");
    }

    public Task<CustomerDto?> FindById(Guid id)
    {
        return db.QuerySingleOrDefaultAsync<CustomerDto>("SELECT * FROM Customers WHERE Id = @Id", new {Id = id});
    }

    public async Task<bool> Update(CustomerDto customer)
    {
        var result = await db.ExecuteAsync(
        """
        UPDATE Customers
        SET Name = @Name, Email = @Email
        WHERE Id = @Id
        """, customer);
        return result > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var result = await db.ExecuteAsync("DELETE FROM Customers WHERE Id = @Id", new { Id = id });
        return result > 0;
    }
}