using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class StockAdjustment : FullAuditedAggregateRoot<Guid>
{
    public StockAdjustment(Guid id) : base(id) { }
    protected StockAdjustment() { }

    public Guid ItemId { get; set; }
    public Guid WarehouseId { get; set; }
    public AdjustmentType AdjustmentType { get; set; }
    public decimal Quantity { get; set; }
    public DateTime AdjustmentDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
