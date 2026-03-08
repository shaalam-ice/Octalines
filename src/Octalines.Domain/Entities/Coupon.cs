using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Coupon : FullAuditedAggregateRoot<Guid>
{
    public Coupon(Guid id) : base(id) { }
    protected Coupon() { }

    public string Code { get; set; } = string.Empty;
    public decimal DiscountAmount { get; set; }
    public decimal? DiscountPercent { get; set; }
    public bool IsPercentage { get; set; } = false;
    public int? MaxUses { get; set; }
    public int UsedCount { get; set; } = 0;
    public DateTime? ExpiryDate { get; set; }
    public bool IsActive { get; set; } = true;
}
