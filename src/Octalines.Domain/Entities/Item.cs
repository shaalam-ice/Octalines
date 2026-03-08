using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Item : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public string? Sku { get; set; }
    public ItemType ItemType { get; set; } = ItemType.Product;
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? UnitId { get; set; }
    public Guid? TaxId { get; set; }
    public decimal CostPrice { get; set; } = 0;
    public decimal SalePrice { get; set; } = 0;
    public decimal StockQuantity { get; set; } = 0;
    public decimal AlertQuantity { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
}
