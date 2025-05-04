namespace DapperTransactions.Contracts.Data;

public class TransactionDto
{
    public required Guid Id { get; set; }
    public required Guid AccountId { get; set; }
    public required decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public int Type { get; set; }
}