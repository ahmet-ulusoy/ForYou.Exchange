using ForYou.Exchange.ProductService.Localization;
using Volo.Abp.Application.Services;

namespace ForYou.Exchange.ProductService;

public abstract class ProductServiceAppService : ApplicationService
{
    protected ProductServiceAppService()
    {
        LocalizationResource = typeof(ProductServiceResource);
        ObjectMapperContext = typeof(ProductServiceApplicationModule);
    }
}
