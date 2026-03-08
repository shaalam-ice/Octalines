using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Octalines;

[DependsOn(
    typeof(AbpIdentityDomainModule),
    typeof(OctalinesDomainSharedModule)
)]
public class OctalinesDomainModule : AbpModule
{
}
