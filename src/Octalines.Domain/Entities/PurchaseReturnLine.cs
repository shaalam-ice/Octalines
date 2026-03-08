using Volo.Abp.Domain.Entities;

namespace Octalines.Entities;

public class PurchaseReturnLine : Entity<Guid>
{
    public PurchaseReturnLine(Guid id) : base(id) { }
    protected PurchaseReturnLine() { }

    public Guid PurchaseReturnId { get; set; }
    public Guid ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}
