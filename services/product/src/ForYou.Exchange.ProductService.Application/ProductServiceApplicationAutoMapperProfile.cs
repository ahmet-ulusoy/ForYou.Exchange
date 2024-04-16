using AutoMapper;
using ForYou.Exchange.ProductService.Products;

namespace ForYou.Exchange.ProductService;

public class ProductServiceApplicationAutoMapperProfile : Profile
{
    public ProductServiceApplicationAutoMapperProfile()
    {
        CreateMap<Product, ProductDto>().MapExtraProperties();
    }
}
