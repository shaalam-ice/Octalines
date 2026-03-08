using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class MoneyTransfer : FullAuditedAggregateRoot<Guid>
{
    public MoneyTransfer(Guid id) : base(id) { }
    protected MoneyTransfer() { }

    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransferDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
