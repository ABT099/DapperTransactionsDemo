using DapperTransactions.Contracts.Data;
using DapperTransactions.Domain;

namespace DapperTransactions.Api.Mapping;

public static class TransactionMapping
{
    public static TransactionDto ToTransactionDto(this Transaction transaction)
    {
        return new TransactionDto
        {
            Id = transaction.Id.Value,
            AccountId = transaction.AccountId.Value,
            Amount = transaction.Amount.Value,
            Description = transaction.Description,
            TransactionDate = transaction.TransactionDate,
            Type = (int) (transaction.Type)
        };
    }
}