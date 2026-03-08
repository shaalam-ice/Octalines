using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Stock.Default)]
public class StockAppService : ApplicationService, IStockAppService
{
    private readonly IRepository<StockAdjustment, Guid> _adjustmentRepo;
    private readonly IRepository<StockTransfer, Guid> _transferRepo;
    private readonly IRepository<StockLedger, Guid> _ledgerRepo;
    private readonly IRepository<Item, Guid> _itemRepo;
    private readonly IRepository<Warehouse, Guid> _warehouseRepo;

    public StockAppService(
        IRepository<StockAdjustment, Guid> adjustmentRepo,
        IRepository<StockTransfer, Guid> transferRepo,
        IRepository<StockLedger, Guid> ledgerRepo,
        IRepository<Item, Guid> itemRepo,
        IRepository<Warehouse, Guid> warehouseRepo)
    {
        _adjustmentRepo = adjustmentRepo;
        _transferRepo = transferRepo;
        _ledgerRepo = ledgerRepo;
        _itemRepo = itemRepo;
        _warehouseRepo = warehouseRepo;
    }

    public async Task<PagedResultDto<StockAdjustmentDto>> GetAdjustmentsAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _adjustmentRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.OrderByDescending(a => a.AdjustmentDate).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        var itemList = await _itemRepo.GetListAsync();
        var warehouseList = await _warehouseRepo.GetListAsync();

        var dtos = items.Select(a =>
        {
            var dto = ObjectMapper.Map<StockAdjustment, StockAdjustmentDto>(a);
            dto.ItemName = itemList.FirstOrDefault(i => i.Id == a.ItemId)?.Name;
            dto.WarehouseName = warehouseList.FirstOrDefault(w => w.Id == a.WarehouseId)?.Name;
            return dto;
        }).ToList();
        return new PagedResultDto<StockAdjustmentDto>(total, dtos);
    }

    [Authorize(OctalinesPermissions.Stock.Create)]
    public async Task<StockAdjustmentDto> CreateAdjustmentAsync(CreateStockAdjustmentDto input)
    {
        var item = await _itemRepo.GetAsync(input.ItemId);
        var balanceBefore = item.StockQuantity;

        if (input.AdjustmentType == AdjustmentType.Increase)
            item.StockQuantity += input.Quantity;
        else
        {
            if (item.StockQuantity < input.Quantity)
                throw new UserFriendlyException($"Cannot decrease stock below zero for item: {item.Name}");
            item.StockQuantity -= input.Quantity;
        }

        await _itemRepo.UpdateAsync(item);

        await _ledgerRepo.InsertAsync(new StockLedger(GuidGenerator.Create())
            {
            ItemId = item.Id,
            WarehouseId = input.WarehouseId,
            MovementType = StockMovementType.ManualAdjustment,
            Quantity = input.AdjustmentType == AdjustmentType.Increase ? input.Quantity : -input.Quantity,
            BalanceBefore = balanceBefore,
            BalanceAfter = item.StockQuantity,
            ReferenceType = "StockAdjustment",
            Notes = input.Notes
        });

        var adjustment = new StockAdjustment(GuidGenerator.Create())
            {
            ItemId = input.ItemId,
            WarehouseId = input.WarehouseId,
            AdjustmentType = input.AdjustmentType,
            Quantity = input.Quantity,
            AdjustmentDate = DateTime.UtcNow,
            Notes = input.Notes
        };
        await _adjustmentRepo.InsertAsync(adjustment, autoSave: true);

        var dto = ObjectMapper.Map<StockAdjustment, StockAdjustmentDto>(adjustment);
        dto.ItemName = item.Name;
        dto.WarehouseName = (await _warehouseRepo.FindAsync(input.WarehouseId))?.Name;
        return dto;
    }

    public async Task<PagedResultDto<StockTransferDto>> GetTransfersAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _transferRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.OrderByDescending(t => t.TransferDate).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        var itemList = await _itemRepo.GetListAsync();
        var warehouseList = await _warehouseRepo.GetListAsync();

        var dtos = items.Select(t =>
        {
            var dto = ObjectMapper.Map<StockTransfer, StockTransferDto>(t);
            dto.ItemName = itemList.FirstOrDefault(i => i.Id == t.ItemId)?.Name;
            dto.FromWarehouseName = warehouseList.FirstOrDefault(w => w.Id == t.FromWarehouseId)?.Name;
            dto.ToWarehouseName = warehouseList.FirstOrDefault(w => w.Id == t.ToWarehouseId)?.Name;
            return dto;
        }).ToList();
        return new PagedResultDto<StockTransferDto>(total, dtos);
    }

    [Authorize(OctalinesPermissions.Stock.Create)]
    public async Task<StockTransferDto> CreateTransferAsync(CreateStockTransferDto input)
    {
        if (input.FromWarehouseId == input.ToWarehouseId)
            throw new UserFriendlyException("Cannot transfer to the same warehouse.");

        var item = await _itemRepo.GetAsync(input.ItemId);
        if (item.StockQuantity < input.Quantity)
            throw new UserFriendlyException($"Insufficient stock for item: {item.Name}");

        var balanceBefore = item.StockQuantity;
        item.StockQuantity -= input.Quantity;
        await _itemRepo.UpdateAsync(item);

        await _ledgerRepo.InsertAsync(new StockLedger(GuidGenerator.Create())
            {
            ItemId = item.Id,
            WarehouseId = input.FromWarehouseId,
            MovementType = StockMovementType.WarehouseTransferOut,
            Quantity = -input.Quantity,
            BalanceBefore = balanceBefore,
            BalanceAfter = item.StockQuantity,
            ReferenceType = "StockTransfer",
            Notes = input.Notes
        });

        var transfer = new StockTransfer(GuidGenerator.Create())
            {
            ItemId = input.ItemId,
            FromWarehouseId = input.FromWarehouseId,
            ToWarehouseId = input.ToWarehouseId,
            Quantity = input.Quantity,
            TransferDate = DateTime.UtcNow,
            Notes = input.Notes
        };
        await _transferRepo.InsertAsync(transfer, autoSave: true);

        var dto = ObjectMapper.Map<StockTransfer, StockTransferDto>(transfer);
        dto.ItemName = item.Name;
        return dto;
    }

    public async Task<PagedResultDto<StockLedgerDto>> GetLedgerAsync(Guid? itemId, PagedAndSortedResultRequestDto input)
    {
        var query = await _ledgerRepo.GetQueryableAsync();
        if (itemId.HasValue) query = query.Where(l => l.ItemId == itemId.Value);
        var total = query.Count();
        var items = query.OrderByDescending(l => l.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        var itemList = await _itemRepo.GetListAsync();
        var warehouseList = await _warehouseRepo.GetListAsync();

        var dtos = items.Select(l =>
        {
            var dto = ObjectMapper.Map<StockLedger, StockLedgerDto>(l);
            dto.ItemName = itemList.FirstOrDefault(i => i.Id == l.ItemId)?.Name;
            dto.WarehouseName = warehouseList.FirstOrDefault(w => w.Id == l.WarehouseId)?.Name;
            return dto;
        }).ToList();
        return new PagedResultDto<StockLedgerDto>(total, dtos);
    }
}
