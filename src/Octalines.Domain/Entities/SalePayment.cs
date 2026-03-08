using Volo.Abp.Domain.Entities;

namespace Octalines.Entities;

public class SalePayment : Entity<Guid>
{
    public Guid SaleId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
