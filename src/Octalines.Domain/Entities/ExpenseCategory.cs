using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class ExpenseCategory : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
