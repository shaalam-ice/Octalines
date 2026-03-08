using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Warehouse : FullAuditedAggregateRoot<Guid>
{
    public Warehouse(Guid id) : base(id) { }
    protected Warehouse() { }

    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public bool IsDefault { get; set; } = false;
}
