using Microsoft.EntityFrameworkCore;
using Octalines.Entities;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Octalines.EntityFrameworkCore;

public class OctalinesDbContextSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly OctalinesDbContext _dbContext;
    private readonly IdentityRoleManager _roleManager;

    public OctalinesDbContextSeedContributor(
        OctalinesDbContext dbContext,
        IdentityRoleManager roleManager)
    {
        _dbContext = dbContext;
        _roleManager = roleManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedRolesAsync();
        await SeedWarehousesAsync();
        await SeedCategoriesAsync();
        await SeedBrandsAsync();
        await SeedUnitsAsync();
        await SeedTaxesAsync();
        await SeedPaymentTypesAsync();
        await SeedAccountsAsync();
        await SeedItemsAsync();
        await SeedStoreSettingsAsync();
        await SeedExpenseCategoriesAsync();
    }

    private async Task SeedRolesAsync()
    {
        string[] roles = { "Admin", "Cashier", "Manager" };
        foreach (var roleName in roles)
        {
            if (await _roleManager.FindByNameAsync(roleName) == null)
            {
                var role = new IdentityRole(Guid.NewGuid(), roleName);
                await _roleManager.CreateAsync(role);
            }
        }
    }

    private async Task SeedWarehousesAsync()
    {
        if (!await _dbContext.Warehouses.AnyAsync())
        {
            _dbContext.Warehouses.Add(new Warehouse(Guid.NewGuid())
            {
                Name = "Main Warehouse",
                Location = "Head Office",
                IsDefault = true
            });
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedCategoriesAsync()
    {
        if (!await _dbContext.Categories.AnyAsync())
        {
            string[] categories = { "Electronics", "Clothing", "Food & Beverage", "Office Supplies", "General" };
            foreach (var name in categories)
                _dbContext.Categories.Add(new Category(Guid.NewGuid()) { Name = name });
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedBrandsAsync()
    {
        if (!await _dbContext.Brands.AnyAsync())
        {
            string[] brands = { "Generic", "Premium", "Local Brand" };
            foreach (var name in brands)
                _dbContext.Brands.Add(new Brand(Guid.NewGuid()) { Name = name });
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedUnitsAsync()
    {
        if (!await _dbContext.Units.AnyAsync())
        {
            var units = new[]
            {
                new { Name = "Piece", Short = "pcs" },
                new { Name = "Kilogram", Short = "kg" },
                new { Name = "Liter", Short = "L" },
                new { Name = "Box", Short = "box" },
                new { Name = "Pack", Short = "pk" }
            };
            foreach (var u in units)
                _dbContext.Units.Add(new Unit(Guid.NewGuid()) { Name = u.Name, ShortName = u.Short });
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedTaxesAsync()
    {
        if (!await _dbContext.Taxes.AnyAsync())
        {
            _dbContext.Taxes.Add(new Tax(Guid.NewGuid()) { Name = "No Tax", Rate = 0, IsActive = true });
            _dbContext.Taxes.Add(new Tax(Guid.NewGuid()) { Name = "VAT 5%", Rate = 5, IsActive = true });
            _dbContext.Taxes.Add(new Tax(Guid.NewGuid()) { Name = "VAT 10%", Rate = 10, IsActive = true });
            _dbContext.Taxes.Add(new Tax(Guid.NewGuid()) { Name = "VAT 15%", Rate = 15, IsActive = true });
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedPaymentTypesAsync()
    {
        if (!await _dbContext.PaymentTypes.AnyAsync())
        {
            string[] types = { "Cash", "Credit Card", "Debit Card", "Mobile Wallet", "Bank Transfer" };
            foreach (var name in types)
                _dbContext.PaymentTypes.Add(new PaymentType(Guid.NewGuid()) { Name = name, IsActive = true });
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedAccountsAsync()
    {
        if (!await _dbContext.Accounts.AnyAsync())
        {
            _dbContext.Accounts.Add(new Account(Guid.NewGuid()) { Name = "Main Cash", AccountType = AccountType.Cash, Balance = 0, IsActive = true });
            _dbContext.Accounts.Add(new Account(Guid.NewGuid()) { Name = "Bank Account", AccountType = AccountType.Bank, Balance = 0, IsActive = true });
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedItemsAsync()
    {
        if (!await _dbContext.Items.AnyAsync())
        {
            var categoryId = await _dbContext.Categories
                .Where(c => c.Name == "General")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            var items = new[]
            {
                new { Name = "Sample Product 1", Barcode = "001", Cost = 10m, Sale = 15m, Stock = 100m },
                new { Name = "Sample Product 2", Barcode = "002", Cost = 20m, Sale = 30m, Stock = 50m },
                new { Name = "Sample Service",   Barcode = "",    Cost = 0m,  Sale = 50m, Stock = 0m }
            };

            foreach (var i in items)
            {
                _dbContext.Items.Add(new Item(Guid.NewGuid())
                {
                    Name = i.Name,
                    Barcode = string.IsNullOrEmpty(i.Barcode) ? null : i.Barcode,
                    ItemType = i.Name.Contains("Service") ? ItemType.Service : ItemType.Product,
                    CategoryId = categoryId == Guid.Empty ? null : categoryId,
                    CostPrice = i.Cost,
                    SalePrice = i.Sale,
                    StockQuantity = i.Stock,
                    AlertQuantity = 10,
                    IsActive = true
                });
            }
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedStoreSettingsAsync()
    {
        if (!await _dbContext.StoreSettings.AnyAsync())
        {
            _dbContext.StoreSettings.Add(new StoreSettings(Guid.NewGuid())
            {
                StoreName = "Octalines Store",
                Currency = "USD",
                AllowNegativeStock = false
            });
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedExpenseCategoriesAsync()
    {
        if (!await _dbContext.ExpenseCategories.AnyAsync())
        {
            string[] cats = { "Rent", "Utilities", "Salaries", "Marketing", "Maintenance", "Other" };
            foreach (var name in cats)
                _dbContext.ExpenseCategories.Add(new ExpenseCategory(Guid.NewGuid()) { Name = name });
            await _dbContext.SaveChangesAsync();
        }
    }
}
