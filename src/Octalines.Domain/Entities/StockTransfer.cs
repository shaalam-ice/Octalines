using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class StockTransfer : FullAuditedAggregateRoot<Guid>
{
    public Guid ItemId { get; set; }
    public Guid FromWarehouseId { get; set; }
    public Guid ToWarehouseId { get; set; }
    public decimal Quantity { get; set; }
    public DateTime TransferDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
