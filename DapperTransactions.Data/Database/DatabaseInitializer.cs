using DbUp;

namespace DapperTransactions.Data.Database;

public class DatabaseInitializer(string connectionString)
{
    public void InitializeDb()
    {
        EnsureDatabase.For.PostgresqlDatabase(connectionString);
        
        var upgrader = DeployChanges.To.PostgresqlDatabase(connectionString)
            .WithScriptsAndCodeEmbeddedInAssembly(typeof(DatabaseInitializer).Assembly)
            .LogToConsole()
            .Build();

        if (upgrader.IsUpgradeRequired())
        {
            var result = upgrader.PerformUpgrade();
        }
    }
}