using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class StockAdjustmentDto : FullAuditedEntityDto<Guid>
{
    public Guid ItemId { get; set; }
    public string? ItemName { get; set; }
    public Guid WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public AdjustmentType AdjustmentType { get; set; }
    public decimal Quantity { get; set; }
    public DateTime AdjustmentDate { get; set; }
    public string? Notes { get; set; }
}

public class CreateStockAdjustmentDto
{
    [Required] public Guid ItemId { get; set; }
    [Required] public Guid WarehouseId { get; set; }
    public AdjustmentType AdjustmentType { get; set; }
    [Range(0.001, double.MaxValue)] public decimal Quantity { get; set; }
    public string? Notes { get; set; }
}

public class StockTransferDto : FullAuditedEntityDto<Guid>
{
    public Guid ItemId { get; set; }
    public string? ItemName { get; set; }
    public Guid FromWarehouseId { get; set; }
    public string? FromWarehouseName { get; set; }
    public Guid ToWarehouseId { get; set; }
    public string? ToWarehouseName { get; set; }
    public decimal Quantity { get; set; }
    public DateTime TransferDate { get; set; }
    public string? Notes { get; set; }
}

public class CreateStockTransferDto
{
    [Required] public Guid ItemId { get; set; }
    [Required] public Guid FromWarehouseId { get; set; }
    [Required] public Guid ToWarehouseId { get; set; }
    [Range(0.001, double.MaxValue)] public decimal Quantity { get; set; }
    public string? Notes { get; set; }
}

public class StockLedgerDto : EntityDto<Guid>
{
    public Guid ItemId { get; set; }
    public string? ItemName { get; set; }
    public Guid WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public StockMovementType MovementType { get; set; }
    public decimal Quantity { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public string? ReferenceType { get; set; }
    public string? Notes { get; set; }
    public DateTime CreationTime { get; set; }
}
