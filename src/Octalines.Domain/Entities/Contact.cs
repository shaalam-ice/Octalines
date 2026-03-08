using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Contact : FullAuditedAggregateRoot<Guid>
{
    public ContactType ContactType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxNumber { get; set; }
    public decimal Balance { get; set; } = 0;
    public bool IsActive { get; set; } = true;
}
