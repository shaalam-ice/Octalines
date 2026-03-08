using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class SaleReturnDto : FullAuditedEntityDto<Guid>
{
    public Guid SaleId { get; set; }
    public string ReturnNumber { get; set; } = string.Empty;
    public DateTime ReturnDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Reason { get; set; }
    public List<SaleReturnLineDto> Lines { get; set; } = new();
}

public class SaleReturnLineDto
{
    public Guid ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}

public class CreateSaleReturnDto
{
    [Required] public Guid SaleId { get; set; }
    public string? Reason { get; set; }
    [Required, MinLength(1)] public List<CreateSaleReturnLineDto> Lines { get; set; } = new();
}

public class CreateSaleReturnLineDto
{
    [Required] public Guid ItemId { get; set; }
    [Range(0.001, double.MaxValue)] public decimal Quantity { get; set; }
    [Range(0, double.MaxValue)] public decimal UnitPrice { get; set; }
}
