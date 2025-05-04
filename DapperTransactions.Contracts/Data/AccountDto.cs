namespace DapperTransactions.Contracts.Data;

public class AccountDto
{
    public required Guid Id { get; set; }
    public required Guid CustomerId { get; set; }
    public required string AccountNumber { get; set; }
    public required decimal Balance { get; set; }
    public required int Type { get; set; }
}