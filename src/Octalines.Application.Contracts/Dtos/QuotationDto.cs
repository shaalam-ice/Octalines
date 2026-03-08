using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class QuotationDto : FullAuditedEntityDto<Guid>
{
    public string QuotationNumber { get; set; } = string.Empty;
    public DateTime QuotationDate { get; set; }
    public Guid? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal GrandTotal { get; set; }
    public bool IsConvertedToSale { get; set; }
    public Guid? SaleId { get; set; }
    public string? Notes { get; set; }
    public List<QuotationLineDto> Lines { get; set; } = new();
}

public class QuotationLineDto
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal LineTotal { get; set; }
}

public class CreateQuotationDto
{
    public DateTime QuotationDate { get; set; } = DateTime.UtcNow;
    public Guid? CustomerId { get; set; }
    public decimal DiscountAmount { get; set; }
    public string? Notes { get; set; }
    [Required, MinLength(1)] public List<CreateQuotationLineDto> Lines { get; set; } = new();
}

public class CreateQuotationLineDto
{
    [Required] public Guid ItemId { get; set; }
    [Range(0.001, double.MaxValue)] public decimal Quantity { get; set; }
    [Range(0, double.MaxValue)] public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; }
}
