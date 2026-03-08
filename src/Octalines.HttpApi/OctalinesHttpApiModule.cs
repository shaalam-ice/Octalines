using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Octalines;

[DependsOn(
    typeof(AbpIdentityHttpApiModule),
    typeof(OctalinesApplicationContractsModule)
)]
public class OctalinesHttpApiModule : AbpModule
{
}
