using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Octalines.Permissions;

namespace Octalines;

[DependsOn(
    typeof(AbpIdentityApplicationContractsModule),
    typeof(OctalinesDomainSharedModule)
)]
public class OctalinesApplicationContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // Permission definition provider is auto-discovered
    }
}
