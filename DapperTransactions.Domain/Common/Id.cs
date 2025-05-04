using ValueOf;

namespace DapperTransactions.Domain.Common;

public class Id : ValueOf<Guid, Id>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException($"{GetType().Name} Id cannot be empty", nameof(Id));
        }
    }
}