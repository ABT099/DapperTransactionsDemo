using DapperTransactions.Domain.Common;

namespace DapperTransactions.Domain;

public class Customer
{
    public required Id Id { get; set; }
    public required string Name { get; set; }
    public required Email Email { get; set; }
}