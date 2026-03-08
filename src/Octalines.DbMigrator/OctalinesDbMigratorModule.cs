using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Octalines.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(OctalinesEntityFrameworkCoreModule),
    typeof(OctalinesApplicationModule)
)]
public class OctalinesDbMigratorModule : AbpModule
{
}
