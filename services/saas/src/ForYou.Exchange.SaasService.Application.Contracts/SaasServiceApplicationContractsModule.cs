﻿using Volo.Abp.Modularity;
using Volo.Saas.Host;
using Volo.Saas.Tenant;
using Volo.Payment.Admin;

namespace ForYou.Exchange.SaasService;

[DependsOn(
    typeof(SaasTenantApplicationContractsModule),
    typeof(SaasHostApplicationContractsModule),
    typeof(SaasServiceDomainSharedModule),
    typeof(AbpPaymentAdminApplicationContractsModule)
)]
public class SaasServiceApplicationContractsModule : AbpModule
{
}
