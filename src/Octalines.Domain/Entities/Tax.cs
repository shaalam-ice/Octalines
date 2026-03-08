using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Tax : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public decimal Rate { get; set; } = 0;
    public bool IsActive { get; set; } = true;
}
