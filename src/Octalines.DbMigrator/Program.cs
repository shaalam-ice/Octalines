using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octalines.DbMigrator;
using Volo.Abp;

using var application = await AbpApplicationFactory.CreateAsync<OctalinesDbMigratorModule>(options =>
{
    options.UseAutofac();
});

await application.InitializeAsync();

var migrationService = application.ServiceProvider.GetRequiredService<OctalinesDbMigratorService>();
await migrationService.RunAsync(CancellationToken.None);

await application.ShutdownAsync();
