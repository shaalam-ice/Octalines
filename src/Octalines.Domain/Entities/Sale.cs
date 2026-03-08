using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Sale : FullAuditedAggregateRoot<Guid>
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
    public Guid? CustomerId { get; set; }
    public Guid WarehouseId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal GrandTotal { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal DueAmount { get; set; }
    public SaleStatus Status { get; set; } = SaleStatus.Draft;
    public string? Notes { get; set; }
    public List<SaleLine> Lines { get; set; } = new();
    public List<SalePayment> Payments { get; set; } = new();
}
