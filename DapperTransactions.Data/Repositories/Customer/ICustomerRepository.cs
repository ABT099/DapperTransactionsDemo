using DapperTransactions.Contracts.Data;

namespace DapperTransactions.Data.Repositories.Customer;

public interface ICustomerRepository
{
    Task<bool> Add(CustomerDto customer);
    Task<IEnumerable<CustomerDto>> FindAll();
    Task<CustomerDto?> FindById(Guid id);
    Task<bool> Update(CustomerDto customer);
    Task<bool> Delete(Guid id);
}