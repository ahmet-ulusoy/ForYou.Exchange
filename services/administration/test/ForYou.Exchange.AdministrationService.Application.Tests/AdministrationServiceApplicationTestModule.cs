using Volo.Abp.Modularity;

namespace ForYou.Exchange.AdministrationService;

[DependsOn(
    typeof(AdministrationServiceApplicationModule),
    typeof(AdministrationServiceDomainTestModule)
    )]
public class AdministrationServiceApplicationTestModule : AbpModule
{

}
