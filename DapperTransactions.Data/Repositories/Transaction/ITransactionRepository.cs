using DapperTransactions.Contracts.Data;

namespace DapperTransactions.Data.Repositories.Transaction;

public interface ITransactionRepository
{
    Task<bool> Add(TransactionDto transaction);
    Task<IEnumerable<TransactionDto>> FindAll();
    Task<TransactionDto?> FindById(Guid id);
    Task<bool> Update(TransactionDto transaction);
    Task<bool> Delete(Guid id);
}