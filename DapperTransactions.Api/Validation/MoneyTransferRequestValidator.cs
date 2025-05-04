using DapperTransactions.Contracts.Requests;
using FluentValidation;

namespace DapperTransactions.Api.Validation;

public class MoneyTransferRequestValidator : AbstractValidator<MoneyTransferRequest>
{
    public MoneyTransferRequestValidator()
    {
        RuleFor(x => x.FromAccountId).NotEmpty();
        RuleFor(x => x.ToAccountId).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Description).MaximumLength(255);
    }
}