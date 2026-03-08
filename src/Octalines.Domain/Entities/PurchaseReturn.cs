using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class PurchaseReturn : FullAuditedAggregateRoot<Guid>
{
    public Guid PurchaseId { get; set; }
    public string ReturnNumber { get; set; } = string.Empty;
    public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string? Reason { get; set; }
    public List<PurchaseReturnLine> Lines { get; set; } = new();
}
