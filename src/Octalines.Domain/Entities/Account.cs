using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Account : FullAuditedAggregateRoot<Guid>
{
    public Account(Guid id) : base(id) { }
    protected Account() { }

    public string Name { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
}
