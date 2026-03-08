using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Quotation : FullAuditedAggregateRoot<Guid>
{
    public string QuotationNumber { get; set; } = string.Empty;
    public DateTime QuotationDate { get; set; } = DateTime.UtcNow;
    public Guid? CustomerId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal GrandTotal { get; set; }
    public bool IsConvertedToSale { get; set; } = false;
    public Guid? SaleId { get; set; }
    public string? Notes { get; set; }
    public List<QuotationLine> Lines { get; set; } = new();
}
