using Volo.Abp.Domain.Entities;

namespace Octalines.Entities;

public class SaleLine : Entity<Guid>
{
    public SaleLine(Guid id) : base(id) { }
    protected SaleLine() { }

    public Guid SaleId { get; set; }
    public Guid ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal LineTotal { get; set; }
}
