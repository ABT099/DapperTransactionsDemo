using DapperTransactions.Api.Attributes;
using DapperTransactions.Api.Mapping;
using DapperTransactions.Contracts.Requests;
using DapperTransactions.Data.Repositories.Account;
using DapperTransactions.Data.Repositories.Transaction;
using DapperTransactions.Domain;
using DapperTransactions.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace DapperTransactions.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankController(
    IAccountRepository accountRepository,
    ITransactionRepository transactionRepository) : ControllerBase
{
    [HttpPost("transfer-money")]
    [Transactional]
    public async Task<IActionResult> TransferMoney(MoneyTransferRequest request)
    {
        var fromAccountDto = await accountRepository.FindById(request.FromAccountId);
        
        if (fromAccountDto is null)
        {
            return NotFound("Source account not found");
        }
        
        var fromAccount = fromAccountDto.ToAccount();

        if (fromAccount.Balance < request.Amount)
        {
            return BadRequest("Insufficient funds");
        }
        
        var toAccountDto = await accountRepository.FindById(request.ToAccountId);

        if (toAccountDto is null)
        {
            return NotFound("Destination account not found");
        }
        
        var toAccount = toAccountDto.ToAccount();
        
        var senderAccountBalance = fromAccount.Balance - request.Amount;
        var receiverAccountBalance = toAccount.Balance + request.Amount;
        
        await accountRepository.UpdateBalance(request.FromAccountId, senderAccountBalance);
        await accountRepository.UpdateBalance(request.ToAccountId, receiverAccountBalance);

        var transactionFrom = new Transaction()
        {
            Id = Id.From(Guid.NewGuid()),
            AccountId = Id.From(request.FromAccountId),
            Amount = TransactionAmount.From(request.Amount),
            Description = request.Description + " (Transfer Out)",
            Type = TransactionType.Transfer,
        };
        
        await transactionRepository.Add(transactionFrom.ToTransactionDto());

        var transactionTo = new Transaction
        {
            Id = Id.From(Guid.NewGuid()),
            AccountId = Id.From(request.ToAccountId),
            Amount = TransactionAmount.From(request.Amount),
            Description = request.Description + " (Transfer In)",
            Type = TransactionType.Transfer,
        };
        
        await transactionRepository.Add(transactionTo.ToTransactionDto());
        
        return Ok();
    }
    
    
    // Nested Transactions
    
    [HttpPost("monthly-operations")]
    [Transactional]
    public async Task<IActionResult> PerformMonthlyOperations()
    {
        await ApplyInterestToSavingsAccounts();
        await ChargeMonthlyFees();
        
        return Ok();
    }

    [Transactional]
    private async Task ApplyInterestToSavingsAccounts()
    {
        var savingsAccountsDtos = await accountRepository.FindAllByType(AccountType.Savings);
        var savingsAccounts = savingsAccountsDtos.ToAccounts();

        foreach (var account in savingsAccounts)
        {
            var interestAmount = Math.Round(account.Balance * 0.002m, 2);
            var newBalance = account.Balance + interestAmount;
            
            await accountRepository.UpdateBalance(account.Id.Value, newBalance);

            var transaction = new Transaction
            {
                Id = Id.From(Guid.NewGuid()),
                AccountId = account.Id,
                Amount = TransactionAmount.From(interestAmount),
                Description = "Monthly interest",
                Type = TransactionType.Deposit,
            };
            
            await transactionRepository.Add(transaction.ToTransactionDto());   
        }
    }

    [Transactional]
    private async Task ChargeMonthlyFees()
    {
        var checkingAccountsDtos  = await accountRepository.FindAllByType(AccountType.Checking);
        var checkingAccounts = checkingAccountsDtos.ToAccounts();

        foreach (var account in checkingAccounts)
        {
            const decimal feeAmount = 5.00m; // $5 monthly fee

            // Skip fee if balance is above $1000
            if (account.Balance >= 1000)
                continue;
            
            var newBalance = account.Balance + feeAmount;
            
            await accountRepository.UpdateBalance(account.Id.Value, newBalance);
        }
    }
    
    
    
    
}