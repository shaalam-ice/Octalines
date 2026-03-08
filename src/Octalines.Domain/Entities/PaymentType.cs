using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class PaymentType : FullAuditedAggregateRoot<Guid>
{
    public PaymentType(Guid id) : base(id) { }
    protected PaymentType() { }

    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
