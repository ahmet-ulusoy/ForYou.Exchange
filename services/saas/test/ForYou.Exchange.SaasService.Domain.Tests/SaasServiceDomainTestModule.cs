using ForYou.Exchange.SaasService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ForYou.Exchange.SaasService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(SaasServiceEntityFrameworkCoreTestModule)
    )]
public class SaasServiceDomainTestModule : AbpModule
{

}
