using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class PurchaseDto : FullAuditedEntityDto<Guid>
{
    public string PurchaseNumber { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public Guid? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public Guid WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal GrandTotal { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal DueAmount { get; set; }
    public PurchaseStatus Status { get; set; }
    public string? Notes { get; set; }
    public List<PurchaseLineDto> Lines { get; set; } = new();
    public List<PurchasePaymentDto> Payments { get; set; } = new();
}

public class PurchaseLineDto
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

public class PurchasePaymentDto
{
    public Guid Id { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Notes { get; set; }
}

public class CreatePurchaseDto
{
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public Guid? SupplierId { get; set; }
    [Required] public Guid WarehouseId { get; set; }
    public decimal DiscountAmount { get; set; }
    public string? Notes { get; set; }
    [Required, MinLength(1)] public List<CreatePurchaseLineDto> Lines { get; set; } = new();
    public List<CreatePurchasePaymentDto> Payments { get; set; } = new();
}

public class CreatePurchaseLineDto
{
    [Required] public Guid ItemId { get; set; }
    [Range(0.001, double.MaxValue)] public decimal Quantity { get; set; }
    [Range(0, double.MaxValue)] public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; }
}

public class CreatePurchasePaymentDto
{
    public PaymentMethod PaymentMethod { get; set; }
    [Range(0.01, double.MaxValue)] public decimal Amount { get; set; }
    public string? Notes { get; set; }
}

public class GetPurchasesInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public Guid? SupplierId { get; set; }
    public PurchaseStatus? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
