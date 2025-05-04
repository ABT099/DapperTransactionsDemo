using DapperTransactions.Contracts.Data;
using DapperTransactions.Domain;

namespace DapperTransactions.Data.Repositories.Account;

public interface IAccountRepository
{
    Task<bool> Add(AccountDto account);
    Task<IEnumerable<AccountDto>> FindAll();
    Task<AccountDto?> FindById(Guid id);
    Task<IEnumerable<AccountDto>> FindAllByType(AccountType type);
    Task<bool> Update(AccountDto account);
    Task<bool> UpdateBalance(Guid id, decimal amount);
    Task<bool> Delete(Guid id);
}