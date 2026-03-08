using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Dashboard.Default)]
public class DashboardAppService : ApplicationService, IDashboardAppService
{
    private readonly IRepository<Sale, Guid> _saleRepo;
    private readonly IRepository<Contact, Guid> _contactRepo;
    private readonly IRepository<Item, Guid> _itemRepo;
    private readonly IRepository<Account, Guid> _accountRepo;
    private readonly IRepository<SaleLine, Guid> _saleLineRepo;

    public DashboardAppService(
        IRepository<Sale, Guid> saleRepo,
        IRepository<Contact, Guid> contactRepo,
        IRepository<Item, Guid> itemRepo,
        IRepository<Account, Guid> accountRepo,
        IRepository<SaleLine, Guid> saleLineRepo)
    {
        _saleRepo = saleRepo;
        _contactRepo = contactRepo;
        _itemRepo = itemRepo;
        _accountRepo = accountRepo;
        _saleLineRepo = saleLineRepo;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var today = DateTime.UtcNow.Date;
        var firstOfMonth = new DateTime(today.Year, today.Month, 1);
        var last30Days = today.AddDays(-30);

        var salesQuery = await _saleRepo.GetQueryableAsync();
        var contactsQuery = await _contactRepo.GetQueryableAsync();
        var itemsQuery = await _itemRepo.GetQueryableAsync();
        var accountsQuery = await _accountRepo.GetQueryableAsync();
        var linesQuery = await _saleLineRepo.GetQueryableAsync();

        var todaySales = salesQuery
            .Where(s => s.InvoiceDate >= today && s.Status == SaleStatus.Completed)
            .Sum(s => (decimal?)s.GrandTotal) ?? 0;

        var monthSales = salesQuery
            .Where(s => s.InvoiceDate >= firstOfMonth && s.Status == SaleStatus.Completed)
            .Sum(s => (decimal?)s.GrandTotal) ?? 0;

        var totalCustomers = contactsQuery.Count(c => c.ContactType == ContactType.Customer && c.IsActive);
        var totalSuppliers = contactsQuery.Count(c => c.ContactType == ContactType.Supplier && c.IsActive);

        var lowStock = itemsQuery.Count(i => i.ItemType == ItemType.Product && i.StockQuantity <= i.AlertQuantity && i.IsActive);
        var cashBalance = accountsQuery.Where(a => a.IsActive).Sum(a => (decimal?)a.Balance) ?? 0;

        var dailySales = salesQuery
            .Where(s => s.InvoiceDate >= last30Days && s.Status == SaleStatus.Completed)
            .GroupBy(s => s.InvoiceDate.Date)
            .Select(g => new DailySalesDto { Date = g.Key, Total = g.Sum(s => s.GrandTotal) })
            .OrderBy(d => d.Date)
            .ToList();

        var topItems = linesQuery
            .GroupBy(l => new { l.ItemId, l.ItemName })
            .Select(g => new TopItemDto
            {
                ItemName = g.Key.ItemName,
                TotalQuantity = g.Sum(l => l.Quantity),
                TotalRevenue = g.Sum(l => l.LineTotal)
            })
            .OrderByDescending(t => t.TotalRevenue)
            .Take(10)
            .ToList();

        return new DashboardSummaryDto
        {
            TodaySales = todaySales,
            ThisMonthSales = monthSales,
            TotalCustomers = totalCustomers,
            TotalSuppliers = totalSuppliers,
            LowStockItems = lowStock,
            CashBalance = cashBalance,
            DailySales = dailySales,
            TopItems = topItems
        };
    }
}
