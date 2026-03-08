using Volo.Abp.Domain.Entities;

namespace Octalines.Entities;

public class PurchaseLine : Entity<Guid>
{
    public PurchaseLine(Guid id) : base(id) { }
    protected PurchaseLine() { }

    public Guid PurchaseId { get; set; }
    public Guid ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal LineTotal { get; set; }
}
