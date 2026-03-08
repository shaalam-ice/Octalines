using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Category : FullAuditedAggregateRoot<Guid>
{
    public Category(Guid id) : base(id) { }
    protected Category() { }

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
