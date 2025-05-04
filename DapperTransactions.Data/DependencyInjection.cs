using DapperTransactions.Data.Database;
using DapperTransactions.Data.DbTransactions;
using DapperTransactions.Data.Repositories.Account;
using DapperTransactions.Data.Repositories.Customer;
using DapperTransactions.Data.Repositories.Transaction;
using Microsoft.Extensions.DependencyInjection;

namespace DapperTransactions.Data;

public static class DependencyInjection
{
    public static void AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(connectionString));
        services.AddSingleton<DatabaseInitializer>(_ => new DatabaseInitializer(connectionString));
        services.AddScoped<ITransactionManager, TransactionManager>();
        services.AddScoped<IDbOperations, DbOperationManager>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
    }
}