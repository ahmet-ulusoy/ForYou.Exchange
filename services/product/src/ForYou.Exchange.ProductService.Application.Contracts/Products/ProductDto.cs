using System;
using Volo.Abp.Application.Dtos;

namespace ForYou.Exchange.ProductService.Products;

public class ProductDto : ExtensibleFullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = default!;

    public float Price { get; set; }
}
