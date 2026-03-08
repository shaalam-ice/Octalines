using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Purchases.Default)]
public class PurchaseAppService : ApplicationService, IPurchaseAppService
{
    private readonly IRepository<Purchase, Guid> _purchaseRepo;
    private readonly IRepository<Item, Guid> _itemRepo;
    private readonly IRepository<Contact, Guid> _contactRepo;
    private readonly IRepository<Warehouse, Guid> _warehouseRepo;
    private readonly IRepository<StockLedger, Guid> _ledgerRepo;

    public PurchaseAppService(
        IRepository<Purchase, Guid> purchaseRepo,
        IRepository<Item, Guid> itemRepo,
        IRepository<Contact, Guid> contactRepo,
        IRepository<Warehouse, Guid> warehouseRepo,
        IRepository<StockLedger, Guid> ledgerRepo)
    {
        _purchaseRepo = purchaseRepo;
        _itemRepo = itemRepo;
        _contactRepo = contactRepo;
        _warehouseRepo = warehouseRepo;
        _ledgerRepo = ledgerRepo;
    }

    public async Task<PagedResultDto<PurchaseDto>> GetListAsync(GetPurchasesInput input)
    {
        var query = await _purchaseRepo.GetQueryableAsync();
        if (!string.IsNullOrWhiteSpace(input.Filter))
            query = query.Where(p => p.PurchaseNumber.Contains(input.Filter));
        if (input.SupplierId.HasValue)
            query = query.Where(p => p.SupplierId == input.SupplierId);
        if (input.Status.HasValue)
            query = query.Where(p => p.Status == input.Status.Value);
        if (input.StartDate.HasValue)
            query = query.Where(p => p.PurchaseDate >= input.StartDate.Value);
        if (input.EndDate.HasValue)
            query = query.Where(p => p.PurchaseDate <= input.EndDate.Value);

        var total = query.Count();
        var purchases = query.OrderByDescending(p => p.PurchaseDate).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        var contacts = await _contactRepo.GetListAsync();
        var warehouses = await _warehouseRepo.GetListAsync();

        var dtos = purchases.Select(p =>
        {
            var dto = ObjectMapper.Map<Purchase, PurchaseDto>(p);
            dto.SupplierName = contacts.FirstOrDefault(c => c.Id == p.SupplierId)?.Name;
            dto.WarehouseName = warehouses.FirstOrDefault(w => w.Id == p.WarehouseId)?.Name;
            return dto;
        }).ToList();

        return new PagedResultDto<PurchaseDto>(total, dtos);
    }

    public async Task<PurchaseDto> GetAsync(Guid id)
    {
        var purchase = await _purchaseRepo.GetAsync(id);
        var dto = ObjectMapper.Map<Purchase, PurchaseDto>(purchase);
        if (purchase.SupplierId.HasValue)
            dto.SupplierName = (await _contactRepo.FindAsync(purchase.SupplierId.Value))?.Name;
        dto.WarehouseName = (await _warehouseRepo.FindAsync(purchase.WarehouseId))?.Name;
        return dto;
    }

    [Authorize(OctalinesPermissions.Purchases.Create)]
    public async Task<PurchaseDto> CreateAsync(CreatePurchaseDto input)
    {
        var purchase = new Purchase(GuidGenerator.Create())
            {
            PurchaseNumber = await GetNextPurchaseNumberAsync(),
            PurchaseDate = input.PurchaseDate,
            SupplierId = input.SupplierId,
            WarehouseId = input.WarehouseId,
            DiscountAmount = input.DiscountAmount,
            Notes = input.Notes,
            Status = PurchaseStatus.Completed
        };

        decimal subTotal = 0;
        foreach (var lineInput in input.Lines)
        {
            var item = await _itemRepo.GetAsync(lineInput.ItemId);
            var lineTotal = (lineInput.Quantity * lineInput.UnitPrice) - lineInput.DiscountAmount;
            subTotal += lineTotal;

            purchase.Lines.Add(new PurchaseLine(GuidGenerator.Create())
                {
                PurchaseId = purchase.Id,
                ItemId = item.Id,
                ItemName = item.Name,
                Quantity = lineInput.Quantity,
                UnitPrice = lineInput.UnitPrice,
                DiscountAmount = lineInput.DiscountAmount,
                LineTotal = lineTotal
            });

            if (item.ItemType == ItemType.Product)
            {
                var balanceBefore = item.StockQuantity;
                item.StockQuantity += lineInput.Quantity;
                await _itemRepo.UpdateAsync(item);

                await _ledgerRepo.InsertAsync(new StockLedger(GuidGenerator.Create())
                    {
                    ItemId = item.Id,
                    WarehouseId = input.WarehouseId,
                    MovementType = StockMovementType.PurchaseAddition,
                    Quantity = lineInput.Quantity,
                    BalanceBefore = balanceBefore,
                    BalanceAfter = item.StockQuantity,
                    ReferenceType = "Purchase",
                    ReferenceId = purchase.Id
                });
            }
        }

        purchase.SubTotal = subTotal;
        purchase.GrandTotal = subTotal - input.DiscountAmount;

        decimal paidAmount = 0;
        foreach (var payInput in input.Payments)
        {
            paidAmount += payInput.Amount;
            purchase.Payments.Add(new PurchasePayment(GuidGenerator.Create())
                {
                PurchaseId = purchase.Id,
                PaymentMethod = payInput.PaymentMethod,
                Amount = payInput.Amount,
                Notes = payInput.Notes
            });
        }

        purchase.PaidAmount = paidAmount;
        purchase.DueAmount = purchase.GrandTotal - paidAmount;

        await _purchaseRepo.InsertAsync(purchase, autoSave: true);
        return await GetAsync(purchase.Id);
    }

    [Authorize(OctalinesPermissions.Purchases.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _purchaseRepo.DeleteAsync(id, autoSave: true);
    }

    public async Task<PurchaseDto> AddPaymentAsync(Guid purchaseId, CreatePurchasePaymentDto input)
    {
        var purchase = await _purchaseRepo.GetAsync(purchaseId);
        purchase.Payments.Add(new PurchasePayment(GuidGenerator.Create())
            {
            PurchaseId = purchase.Id,
            PaymentMethod = input.PaymentMethod,
            Amount = input.Amount,
            Notes = input.Notes,
            PaymentDate = DateTime.UtcNow
        });
        purchase.PaidAmount += input.Amount;
        purchase.DueAmount = purchase.GrandTotal - purchase.PaidAmount;
        await _purchaseRepo.UpdateAsync(purchase, autoSave: true);
        return await GetAsync(purchase.Id);
    }

    public async Task<string> GetNextPurchaseNumberAsync()
    {
        var query = await _purchaseRepo.GetQueryableAsync();
        var count = query.Count();
        return "PO-" + (count + 1).ToString("D6");
    }
}
