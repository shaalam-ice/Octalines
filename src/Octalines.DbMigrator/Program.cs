using Octalines;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

using var application = await AbpApplicationFactory.CreateAsync<OctalinesDbMigratorModule>(options =>
{
    options.UseAutofac();
});

await application.InitializeAsync();

var migrationService = application.ServiceProvider.GetRequiredService<OctalinesDbMigratorService>();
await migrationService.RunAsync();

await application.ShutdownAsync();
