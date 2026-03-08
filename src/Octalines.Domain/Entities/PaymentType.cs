using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class PaymentType : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
