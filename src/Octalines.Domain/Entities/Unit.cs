using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Unit : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? ShortName { get; set; }
}
