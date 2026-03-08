using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Account : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
}
