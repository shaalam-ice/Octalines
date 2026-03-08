using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class CustomerCoupon : FullAuditedAggregateRoot<Guid>
{
    public CustomerCoupon(Guid id) : base(id) { }
    protected CustomerCoupon() { }

    public Guid CustomerId { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal DiscountAmount { get; set; }
    public bool IsUsed { get; set; } = false;
    public DateTime? ExpiryDate { get; set; }
    public Guid? UsedInSaleId { get; set; }
}
