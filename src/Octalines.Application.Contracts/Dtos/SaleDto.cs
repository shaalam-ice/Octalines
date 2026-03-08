using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class SaleDto : FullAuditedEntityDto<Guid>
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public Guid? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public Guid WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal GrandTotal { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal DueAmount { get; set; }
    public SaleStatus Status { get; set; }
    public string? Notes { get; set; }
    public List<SaleLineDto> Lines { get; set; } = new();
    public List<SalePaymentDto> Payments { get; set; } = new();
}

public class SaleLineDto
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

public class SalePaymentDto
{
    public Guid Id { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Notes { get; set; }
}

public class CreateSaleDto
{
    public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
    public Guid? CustomerId { get; set; }
    [Required] public Guid WarehouseId { get; set; }
    public decimal DiscountAmount { get; set; }
    public string? Notes { get; set; }
    [Required, MinLength(1)] public List<CreateSaleLineDto> Lines { get; set; } = new();
    public List<CreateSalePaymentDto> Payments { get; set; } = new();
}

public class CreateSaleLineDto
{
    [Required] public Guid ItemId { get; set; }
    [Range(0.001, double.MaxValue)] public decimal Quantity { get; set; }
    [Range(0, double.MaxValue)] public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; }
}

public class CreateSalePaymentDto
{
    public PaymentMethod PaymentMethod { get; set; }
    [Range(0.01, double.MaxValue)] public decimal Amount { get; set; }
    public string? Notes { get; set; }
}

public class GetSalesInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public Guid? CustomerId { get; set; }
    public SaleStatus? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class AddSalePaymentDto
{
    [Required] public Guid SaleId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    [Range(0.01, double.MaxValue)] public decimal Amount { get; set; }
    public string? Notes { get; set; }
}
