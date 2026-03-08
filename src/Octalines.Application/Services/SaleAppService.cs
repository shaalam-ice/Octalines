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

[Authorize(OctalinesPermissions.Sales.Default)]
public class SaleAppService : ApplicationService, ISaleAppService
{
    private readonly IRepository<Sale, Guid> _saleRepo;
    private readonly IRepository<SaleReturn, Guid> _saleReturnRepo;
    private readonly IRepository<Item, Guid> _itemRepo;
    private readonly IRepository<Contact, Guid> _contactRepo;
    private readonly IRepository<Warehouse, Guid> _warehouseRepo;
    private readonly IRepository<StockLedger, Guid> _ledgerRepo;
    private readonly IRepository<StoreSettings, Guid> _settingsRepo;

    public SaleAppService(
        IRepository<Sale, Guid> saleRepo,
        IRepository<SaleReturn, Guid> saleReturnRepo,
        IRepository<Item, Guid> itemRepo,
        IRepository<Contact, Guid> contactRepo,
        IRepository<Warehouse, Guid> warehouseRepo,
        IRepository<StockLedger, Guid> ledgerRepo,
        IRepository<StoreSettings, Guid> settingsRepo)
    {
        _saleRepo = saleRepo;
        _saleReturnRepo = saleReturnRepo;
        _itemRepo = itemRepo;
        _contactRepo = contactRepo;
        _warehouseRepo = warehouseRepo;
        _ledgerRepo = ledgerRepo;
        _settingsRepo = settingsRepo;
    }

    public async Task<PagedResultDto<SaleDto>> GetListAsync(GetSalesInput input)
    {
        var query = await _saleRepo.GetQueryableAsync();
        if (!string.IsNullOrWhiteSpace(input.Filter))
            query = query.Where(s => s.InvoiceNumber.Contains(input.Filter));
        if (input.CustomerId.HasValue)
            query = query.Where(s => s.CustomerId == input.CustomerId);
        if (input.Status.HasValue)
            query = query.Where(s => s.Status == input.Status.Value);
        if (input.StartDate.HasValue)
            query = query.Where(s => s.InvoiceDate >= input.StartDate.Value);
        if (input.EndDate.HasValue)
            query = query.Where(s => s.InvoiceDate <= input.EndDate.Value);

        var total = query.Count();
        var sales = query.OrderByDescending(s => s.InvoiceDate)
            .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        var contacts = await _contactRepo.GetListAsync();
        var warehouses = await _warehouseRepo.GetListAsync();

        var dtos = sales.Select(s =>
        {
            var dto = ObjectMapper.Map<Sale, SaleDto>(s);
            dto.CustomerName = contacts.FirstOrDefault(c => c.Id == s.CustomerId)?.Name;
            dto.WarehouseName = warehouses.FirstOrDefault(w => w.Id == s.WarehouseId)?.Name;
            return dto;
        }).ToList();

        return new PagedResultDto<SaleDto>(total, dtos);
    }

    public async Task<SaleDto> GetAsync(Guid id)
    {
        var sale = await _saleRepo.GetAsync(id);
        var dto = ObjectMapper.Map<Sale, SaleDto>(sale);
        if (sale.CustomerId.HasValue)
            dto.CustomerName = (await _contactRepo.FindAsync(sale.CustomerId.Value))?.Name;
        dto.WarehouseName = (await _warehouseRepo.FindAsync(sale.WarehouseId))?.Name;
        return dto;
    }

    [Authorize(OctalinesPermissions.Sales.Create)]
    public async Task<SaleDto> CreateAsync(CreateSaleDto input)
    {
        var settingsList = await _settingsRepo.GetListAsync();
        var settings = settingsList.FirstOrDefault();
        bool allowNegative = settings?.AllowNegativeStock ?? false;

        var sale = new Sale(GuidGenerator.Create())
            {
            InvoiceNumber = await GetNextInvoiceNumberAsync(),
            InvoiceDate = input.InvoiceDate,
            CustomerId = input.CustomerId,
            WarehouseId = input.WarehouseId,
            DiscountAmount = input.DiscountAmount,
            Notes = input.Notes,
            Status = SaleStatus.Completed
        };

        decimal subTotal = 0;
        decimal totalTax = 0;

        foreach (var lineInput in input.Lines)
        {
            var item = await _itemRepo.GetAsync(lineInput.ItemId);

            if (item.ItemType == ItemType.Product && !allowNegative && item.StockQuantity < lineInput.Quantity)
                throw new UserFriendlyException($"Insufficient stock for item: {item.Name}. Available: {item.StockQuantity}");

            var lineTotal = (lineInput.Quantity * lineInput.UnitPrice) - lineInput.DiscountAmount;
            subTotal += lineTotal;

            var line = new SaleLine(GuidGenerator.Create())
                {
                SaleId = sale.Id,
                ItemId = item.Id,
                ItemName = item.Name,
                Quantity = lineInput.Quantity,
                UnitPrice = lineInput.UnitPrice,
                DiscountAmount = lineInput.DiscountAmount,
                LineTotal = lineTotal
            };
            sale.Lines.Add(line);

            if (item.ItemType == ItemType.Product)
            {
                var balanceBefore = item.StockQuantity;
                item.StockQuantity -= lineInput.Quantity;
                await _itemRepo.UpdateAsync(item);

                await _ledgerRepo.InsertAsync(new StockLedger(GuidGenerator.Create())
                    {
                    ItemId = item.Id,
                    WarehouseId = input.WarehouseId,
                    MovementType = StockMovementType.SaleDeduction,
                    Quantity = -lineInput.Quantity,
                    BalanceBefore = balanceBefore,
                    BalanceAfter = item.StockQuantity,
                    ReferenceType = "Sale",
                    ReferenceId = sale.Id
                });
            }
        }

        sale.SubTotal = subTotal;
        sale.TaxAmount = totalTax;
        sale.GrandTotal = subTotal + totalTax - input.DiscountAmount;

        decimal paidAmount = 0;
        foreach (var payInput in input.Payments)
        {
            paidAmount += payInput.Amount;
            sale.Payments.Add(new SalePayment(GuidGenerator.Create())
                {
                SaleId = sale.Id,
                PaymentMethod = payInput.PaymentMethod,
                Amount = payInput.Amount,
                Notes = payInput.Notes
            });
        }

        sale.PaidAmount = paidAmount;
        sale.DueAmount = sale.GrandTotal - paidAmount;

        await _saleRepo.InsertAsync(sale, autoSave: true);
        return await GetAsync(sale.Id);
    }

    [Authorize(OctalinesPermissions.Sales.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _saleRepo.DeleteAsync(id, autoSave: true);
    }

    public async Task<SaleDto> AddPaymentAsync(AddSalePaymentDto input)
    {
        var sale = await _saleRepo.GetAsync(input.SaleId);
        sale.Payments.Add(new SalePayment(GuidGenerator.Create())
            {
            SaleId = sale.Id,
            PaymentMethod = input.PaymentMethod,
            Amount = input.Amount,
            Notes = input.Notes,
            PaymentDate = DateTime.UtcNow
        });
        sale.PaidAmount += input.Amount;
        sale.DueAmount = sale.GrandTotal - sale.PaidAmount;
        await _saleRepo.UpdateAsync(sale, autoSave: true);
        return await GetAsync(sale.Id);
    }

    public async Task<SaleReturnDto> CreateReturnAsync(CreateSaleReturnDto input)
    {
        var sale = await _saleRepo.GetAsync(input.SaleId);
        var returnObj = new SaleReturn(GuidGenerator.Create())
            {
            SaleId = input.SaleId,
            ReturnNumber = "RET-" + DateTime.UtcNow.Ticks.ToString()[^6..],
            ReturnDate = DateTime.UtcNow,
            Reason = input.Reason
        };

        decimal total = 0;
        foreach (var lineInput in input.Lines)
        {
            var item = await _itemRepo.GetAsync(lineInput.ItemId);
            var lineTotal = lineInput.Quantity * lineInput.UnitPrice;
            total += lineTotal;

            returnObj.Lines.Add(new SaleReturnLine(GuidGenerator.Create())
                {
                SaleReturnId = returnObj.Id,
                ItemId = item.Id,
                ItemName = item.Name,
                Quantity = lineInput.Quantity,
                UnitPrice = lineInput.UnitPrice,
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
                    WarehouseId = sale.WarehouseId,
                    MovementType = StockMovementType.SaleReturn,
                    Quantity = lineInput.Quantity,
                    BalanceBefore = balanceBefore,
                    BalanceAfter = item.StockQuantity,
                    ReferenceType = "SaleReturn",
                    ReferenceId = returnObj.Id
                });
            }
        }

        returnObj.TotalAmount = total;
        sale.Status = SaleStatus.Returned;
        await _saleRepo.UpdateAsync(sale);
        await _saleReturnRepo.InsertAsync(returnObj, autoSave: true);
        return ObjectMapper.Map<SaleReturn, SaleReturnDto>(returnObj);
    }

    public async Task<PagedResultDto<SaleReturnDto>> GetReturnsAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _saleReturnRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.OrderByDescending(r => r.ReturnDate).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<SaleReturnDto>(total, ObjectMapper.Map<List<SaleReturn>, List<SaleReturnDto>>(items));
    }

    public async Task<string> GetNextInvoiceNumberAsync()
    {
        var query = await _saleRepo.GetQueryableAsync();
        var count = query.Count();
        return "INV-" + (count + 1).ToString("D6");
    }
}
