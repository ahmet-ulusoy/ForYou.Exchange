using ForYou.Exchange.SaasService.Application;
using Volo.Abp.Modularity;

namespace ForYou.Exchange.SaasService;

[DependsOn(
    typeof(SaasServiceApplicationModule),
    typeof(SaasServiceDomainTestModule)
    )]
public class SaasServiceApplicationTestModule : AbpModule
{

}
