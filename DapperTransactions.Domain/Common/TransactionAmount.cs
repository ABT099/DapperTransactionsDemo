using ValueOf;

namespace DapperTransactions.Domain.Common;

public class TransactionAmount : ValueOf<decimal, TransactionAmount>
{
    protected override void Validate()
    {
        if (Value <= 0)
        {
            throw new ArgumentException("Transaction amount must be greater than zero", nameof(TransactionAmount));
        }
    }
}