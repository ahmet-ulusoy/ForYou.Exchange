using ForYou.Exchange.ProductService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ForYou.Exchange.ProductService;

public abstract class ProductServiceController : AbpController
{
    protected ProductServiceController()
    {
        LocalizationResource = typeof(ProductServiceResource);
    }
}
