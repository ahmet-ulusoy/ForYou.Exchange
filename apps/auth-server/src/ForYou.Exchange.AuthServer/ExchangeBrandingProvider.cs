using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ForYou.Exchange.AuthServer;

[Dependency(ReplaceServices = true)]
public class ExchangeBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Exchange";
}
