using ForYou.Exchange.AdministrationService;
using ForYou.Exchange.AdministrationService.EntityFrameworkCore;
using ForYou.Exchange.IdentityService;
using ForYou.Exchange.IdentityService.EntityFrameworkCore;
using ForYou.Exchange.ProductService;
using ForYou.Exchange.ProductService.EntityFrameworkCore;
using ForYou.Exchange.SaasService;
using ForYou.Exchange.SaasService.EntityFrameworkCore;
using ForYou.Exchange.Shared.Hosting;
using Volo.Abp.Modularity;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;

namespace ForYou.Exchange.DbMigrator;

[DependsOn(
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(ExchangeSharedHostingModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(SaasServiceEntityFrameworkCoreModule),
    typeof(SaasServiceApplicationContractsModule),
    typeof(AdministrationServiceEntityFrameworkCoreModule),
    typeof(AdministrationServiceApplicationContractsModule),
    typeof(ProductServiceApplicationContractsModule),
    typeof(ProductServiceEntityFrameworkCoreModule)
)]
public class ExchangeDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Exchange:"; });
    }
}
