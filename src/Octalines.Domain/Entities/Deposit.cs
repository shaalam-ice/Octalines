using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Deposit : FullAuditedAggregateRoot<Guid>
{
    public Deposit(Guid id) : base(id) { }
    protected Deposit() { }

    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DepositDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
