using Volo.Abp.Domain.Entities;

namespace Octalines.Entities;

public class PurchasePayment : Entity<Guid>
{
    public Guid PurchaseId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
