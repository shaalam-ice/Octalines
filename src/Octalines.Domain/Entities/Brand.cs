using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Brand : FullAuditedAggregateRoot<Guid>
{
    public Brand(Guid id) : base(id) { }
    protected Brand() { }

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
