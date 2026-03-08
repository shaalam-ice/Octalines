using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Octalines;

[DependsOn(
    typeof(AbpIdentityDomainSharedModule)
)]
public class OctalinesDomainSharedModule : AbpModule
{
}
