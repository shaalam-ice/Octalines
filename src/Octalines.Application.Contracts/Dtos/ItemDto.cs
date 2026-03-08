using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class ItemDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public string? Sku { get; set; }
    public ItemType ItemType { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public Guid? BrandId { get; set; }
    public string? BrandName { get; set; }
    public Guid? UnitId { get; set; }
    public string? UnitName { get; set; }
    public Guid? TaxId { get; set; }
    public decimal TaxRate { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SalePrice { get; set; }
    public decimal StockQuantity { get; set; }
    public decimal AlertQuantity { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
}

public class CreateUpdateItemDto
{
    [Required] public string Name { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public string? Sku { get; set; }
    public ItemType ItemType { get; set; } = ItemType.Product;
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? UnitId { get; set; }
    public Guid? TaxId { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SalePrice { get; set; }
    public decimal AlertQuantity { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Description { get; set; }
}

public class GetItemsInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public Guid? CategoryId { get; set; }
    public ItemType? ItemType { get; set; }
    public bool? IsActive { get; set; }
    public bool? LowStock { get; set; }
}
