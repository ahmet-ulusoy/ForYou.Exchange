using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Gdpr;
using Volo.Abp.LanguageManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Chat;

namespace ForYou.Exchange.AdministrationService;

[DependsOn(
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpAuditLoggingApplicationModule),
    typeof(LanguageManagementApplicationModule),
    typeof(TextTemplateManagementApplicationModule),
    typeof(AbpGdprApplicationModule),
    typeof(AdministrationServiceApplicationContractsModule),
    typeof(AdministrationServiceDomainModule)
)]
[DependsOn(typeof(ChatApplicationModule))]
    public class AdministrationServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AdministrationServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AdministrationServiceApplicationModule>(validate: true);
        });
    }
}
