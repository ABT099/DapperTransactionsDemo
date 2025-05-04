using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace DapperTransactions.Domain.Common;

public partial class Email : ValueOf<string, Email>
{
    private static readonly Regex EmailRegex = MyRegex();

    protected override void Validate()
    {
        if (EmailRegex.IsMatch(Value))
        {
            return;
        }
        
        var message = $"{Value} is not a valid email address";
        throw new ValidationException(message, [
            new ValidationFailure(nameof(Email), message)
        ]);
    }

    // This will be generated at compile time this is why the class is partial
    [GeneratedRegex(@"^[\w!#$%&’*+/=?`{|}~^-]+(?:\.[\w!#$%&’*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,6}$", 
        RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-001")]
    private static partial Regex MyRegex();
}