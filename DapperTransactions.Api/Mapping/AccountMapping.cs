using DapperTransactions.Domain;
using DapperTransactions.Domain.Common;
using DapperTransactions.Contracts.Data;

namespace DapperTransactions.Api.Mapping;

public static class AccountMapping
{
    public static Account ToAccount(this AccountDto accountDto)
    {
        return new Account
        {
            Id = Id.From(accountDto.Id),
            Balance = accountDto.Balance,
            Type = (AccountType)accountDto.Type,
            CustomerId = Id.From(accountDto.CustomerId),
            AccountNumber = accountDto.AccountNumber,
        };
    }

    public static IEnumerable<Account> ToAccounts(this IEnumerable<AccountDto> accountDtos)
    {
        return accountDtos.Select(dto => dto.ToAccount());
    }
}