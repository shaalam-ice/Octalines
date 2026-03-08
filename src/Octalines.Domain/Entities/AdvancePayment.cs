using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class AdvancePayment : FullAuditedAggregateRoot<Guid>
{
    public Guid ContactId { get; set; }
    public AdvanceType AdvanceType { get; set; }
    public decimal Amount { get; set; }
    public decimal UsedAmount { get; set; } = 0;
    public decimal RemainingAmount { get; set; }
    public DateTime AdvanceDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
