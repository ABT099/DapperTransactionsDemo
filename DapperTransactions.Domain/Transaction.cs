using DapperTransactions.Domain.Common;

namespace DapperTransactions.Domain;

public class Transaction
{
    public required Id Id { get; set; }
    public required Id AccountId { get; set; }
    public required TransactionAmount Amount { get; set; }
    public string? Description { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    public TransactionType Type { get; set; }
}

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}
