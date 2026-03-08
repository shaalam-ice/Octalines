using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Purchase : FullAuditedAggregateRoot<Guid>
{
    public Purchase(Guid id) : base(id) { }
    protected Purchase() { }

    public string PurchaseNumber { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public Guid? SupplierId { get; set; }
    public Guid WarehouseId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal GrandTotal { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal DueAmount { get; set; }
    public PurchaseStatus Status { get; set; } = PurchaseStatus.Draft;
    public string? Notes { get; set; }
    public List<PurchaseLine> Lines { get; set; } = new();
    public List<PurchasePayment> Payments { get; set; } = new();
}
