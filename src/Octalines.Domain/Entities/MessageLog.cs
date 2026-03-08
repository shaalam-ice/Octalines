using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class MessageLog : CreationAuditedAggregateRoot<Guid>
{
    public MessageLog(Guid id) : base(id) { }
    protected MessageLog() { }

    public string RecipientPhone { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public MessageChannel Channel { get; set; }
    public bool IsSuccess { get; set; } = false;
    public string? ErrorMessage { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}
