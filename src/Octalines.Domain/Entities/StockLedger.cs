using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class StockLedger : CreationAuditedAggregateRoot<Guid>
{
    public Guid ItemId { get; set; }
    public Guid WarehouseId { get; set; }
    public StockMovementType MovementType { get; set; }
    public decimal Quantity { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public string? ReferenceType { get; set; }
    public Guid? ReferenceId { get; set; }
    public string? Notes { get; set; }
}
