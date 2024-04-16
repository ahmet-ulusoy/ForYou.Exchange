using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ForYou.Exchange.PublicWeb;

[Dependency(ReplaceServices = true)]
public class ExchangeBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Exchange";
}
