using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Octalines;

[DependsOn(
    typeof(AbpIdentityApplicationContractsModule),
    typeof(OctalinesDomainSharedModule)
)]
public class OctalinesApplicationContractsModule : AbpModule
{
}
