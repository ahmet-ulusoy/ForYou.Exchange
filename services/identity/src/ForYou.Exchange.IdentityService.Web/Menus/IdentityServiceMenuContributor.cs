using System.Threading.Tasks;
using ForYou.Exchange.IdentityService.Localization;
using Volo.Abp.UI.Navigation;

namespace ForYou.Exchange.IdentityService.Web.Menus;

public class IdentityServiceMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<IdentityServiceResource>();
        return Task.CompletedTask;
    }
}
