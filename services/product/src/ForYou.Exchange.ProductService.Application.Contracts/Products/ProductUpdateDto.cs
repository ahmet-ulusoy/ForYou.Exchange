using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace ForYou.Exchange.ProductService.Products;

public class ProductUpdateDto: ExtensibleObject
{
    [Required]
    [StringLength(ProductConsts.NameMaxLength)]
    public string Name { get; set; } = default!;

    [Required]
    public float Price { get; set; }
}
