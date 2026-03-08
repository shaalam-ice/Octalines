using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Octalines.DbMigrator;

public class OctalinesDbMigratorService : ITransientDependency
{
    private readonly IDataSeeder _dataSeeder;

    public OctalinesDbMigratorService(IDataSeeder dataSeeder)
    {
        _dataSeeder = dataSeeder;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting database migration and seeding...");
        await _dataSeeder.SeedAsync();
        Console.WriteLine("Done! Database migration and seeding completed.");
    }
}
