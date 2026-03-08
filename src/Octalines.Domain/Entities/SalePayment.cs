using Volo.Abp.Domain.Entities;

namespace Octalines.Entities;

public class SalePayment : Entity<Guid>
{
    public SalePayment(Guid id) : base(id) { }
    protected SalePayment() { }

    public Guid SaleId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
}
