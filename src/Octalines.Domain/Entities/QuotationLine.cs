using Volo.Abp.Domain.Entities;

namespace Octalines.Entities;

public class QuotationLine : Entity<Guid>
{
    public QuotationLine(Guid id) : base(id) { }
    protected QuotationLine() { }

    public Guid QuotationId { get; set; }
    public Guid ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal LineTotal { get; set; }
}
