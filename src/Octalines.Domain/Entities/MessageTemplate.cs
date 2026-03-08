using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class MessageTemplate : FullAuditedAggregateRoot<Guid>
{
    public MessageTemplate(Guid id) : base(id) { }
    protected MessageTemplate() { }

    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public MessageChannel Channel { get; set; }
    public bool IsActive { get; set; } = true;
}
