using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class CouponDto : FullAuditedEntityDto<Guid>
{
    public string Code { get; set; } = string.Empty;
    public decimal DiscountAmount { get; set; }
    public decimal? DiscountPercent { get; set; }
    public bool IsPercentage { get; set; }
    public int? MaxUses { get; set; }
    public int UsedCount { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsActive { get; set; }
}

public class CreateUpdateCouponDto
{
    [Required] public string Code { get; set; } = string.Empty;
    public decimal DiscountAmount { get; set; }
    public decimal? DiscountPercent { get; set; }
    public bool IsPercentage { get; set; }
    public int? MaxUses { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsActive { get; set; } = true;
}

public class CustomerCouponDto : FullAuditedEntityDto<Guid>
{
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal DiscountAmount { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

public class CreateCustomerCouponDto
{
    [Required] public Guid CustomerId { get; set; }
    [Required] public string Code { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue)] public decimal DiscountAmount { get; set; }
    public DateTime? ExpiryDate { get; set; }
}
