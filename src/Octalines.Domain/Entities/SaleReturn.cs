using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class SaleReturn : FullAuditedAggregateRoot<Guid>
{
    public SaleReturn(Guid id) : base(id) { }
    protected SaleReturn() { }

    public Guid SaleId { get; set; }
    public string ReturnNumber { get; set; } = string.Empty;
    public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string? Reason { get; set; }
    public List<SaleReturnLine> Lines { get; set; } = new();
}
