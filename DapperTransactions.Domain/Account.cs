using DapperTransactions.Domain.Common;

namespace DapperTransactions.Domain;

public class Account
{
    public required Id Id { get; set; }
    public required Id CustomerId { get; set; }
    public required string AccountNumber { get; set; }
    public required decimal Balance { get; set; }
    public required AccountType Type { get; set; }
}

public enum AccountType
{
    Checking,
    Savings
}