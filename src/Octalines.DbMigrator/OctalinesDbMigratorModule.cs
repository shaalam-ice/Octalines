using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Octalines;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(OctalinesApplicationModule),
    typeof(OctalinesEntityFrameworkCoreModule)
)]
public class OctalinesDbMigratorModule : AbpModule
{
}
