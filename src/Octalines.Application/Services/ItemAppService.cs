using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Items.Default)]
public class ItemAppService : ApplicationService, IItemAppService
{
    private readonly IRepository<Item, Guid> _itemRepo;
    private readonly IRepository<Category, Guid> _categoryRepo;
    private readonly IRepository<Brand, Guid> _brandRepo;
    private readonly IRepository<Unit, Guid> _unitRepo;
    private readonly IRepository<Tax, Guid> _taxRepo;

    public ItemAppService(
        IRepository<Item, Guid> itemRepo,
        IRepository<Category, Guid> categoryRepo,
        IRepository<Brand, Guid> brandRepo,
        IRepository<Unit, Guid> unitRepo,
        IRepository<Tax, Guid> taxRepo)
    {
        _itemRepo = itemRepo;
        _categoryRepo = categoryRepo;
        _brandRepo = brandRepo;
        _unitRepo = unitRepo;
        _taxRepo = taxRepo;
    }

    public async Task<PagedResultDto<ItemDto>> GetListAsync(GetItemsInput input)
    {
        var query = await _itemRepo.GetQueryableAsync();
        if (!string.IsNullOrWhiteSpace(input.Filter))
            query = query.Where(i => i.Name.Contains(input.Filter) || (i.Barcode != null && i.Barcode.Contains(input.Filter)));
        if (input.CategoryId.HasValue)
            query = query.Where(i => i.CategoryId == input.CategoryId);
        if (input.ItemType.HasValue)
            query = query.Where(i => i.ItemType == input.ItemType.Value);
        if (input.IsActive.HasValue)
            query = query.Where(i => i.IsActive == input.IsActive.Value);
        if (input.LowStock == true)
            query = query.Where(i => i.StockQuantity <= i.AlertQuantity && i.ItemType == ItemType.Product);

        var total = query.Count();
        var items = query.OrderBy(i => i.Name).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        var categories = await _categoryRepo.GetListAsync();
        var brands = await _brandRepo.GetListAsync();
        var units = await _unitRepo.GetListAsync();
        var taxes = await _taxRepo.GetListAsync();

        var dtos = items.Select(item =>
        {
            var dto = ObjectMapper.Map<Item, ItemDto>(item);
            dto.CategoryName = categories.FirstOrDefault(c => c.Id == item.CategoryId)?.Name;
            dto.BrandName = brands.FirstOrDefault(b => b.Id == item.BrandId)?.Name;
            dto.UnitName = units.FirstOrDefault(u => u.Id == item.UnitId)?.Name;
            dto.TaxRate = taxes.FirstOrDefault(t => t.Id == item.TaxId)?.Rate ?? 0;
            return dto;
        }).ToList();

        return new PagedResultDto<ItemDto>(total, dtos);
    }

    public async Task<ItemDto> GetAsync(Guid id)
    {
        var item = await _itemRepo.GetAsync(id);
        var dto = ObjectMapper.Map<Item, ItemDto>(item);
        if (item.CategoryId.HasValue)
            dto.CategoryName = (await _categoryRepo.FindAsync(item.CategoryId.Value))?.Name;
        if (item.BrandId.HasValue)
            dto.BrandName = (await _brandRepo.FindAsync(item.BrandId.Value))?.Name;
        if (item.UnitId.HasValue)
            dto.UnitName = (await _unitRepo.FindAsync(item.UnitId.Value))?.Name;
        if (item.TaxId.HasValue)
            dto.TaxRate = (await _taxRepo.FindAsync(item.TaxId.Value))?.Rate ?? 0;
        return dto;
    }

    [Authorize(OctalinesPermissions.Items.Create)]
    public async Task<ItemDto> CreateAsync(CreateUpdateItemDto input)
    {
        var item = new Item(GuidGenerator.Create());
        ObjectMapper.Map(input, item);
        await _itemRepo.InsertAsync(item, autoSave: true);
        return await GetAsync(item.Id);
    }

    [Authorize(OctalinesPermissions.Items.Edit)]
    public async Task<ItemDto> UpdateAsync(Guid id, CreateUpdateItemDto input)
    {
        var item = await _itemRepo.GetAsync(id);
        ObjectMapper.Map(input, item);
        await _itemRepo.UpdateAsync(item, autoSave: true);
        return await GetAsync(id);
    }

    [Authorize(OctalinesPermissions.Items.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _itemRepo.DeleteAsync(id, autoSave: true);
    }

    public async Task ImportFromCsvAsync(Stream csvStream, ItemType itemType)
    {
        using var reader = new StreamReader(csvStream);
        await reader.ReadLineAsync(); // skip header
        string? line;
        var items = new List<Item>();
        while ((line = await reader.ReadLineAsync()) != null)
        {
            var parts = line.Split(',');
            if (parts.Length < 1) continue;
            decimal.TryParse(parts.Length > 3 ? parts[3].Trim() : "0", out var cost);
            decimal.TryParse(parts.Length > 4 ? parts[4].Trim() : "0", out var sale);
            items.Add(new Item(GuidGenerator.Create())
                {
                Name = parts[0].Trim(),
                Barcode = parts.Length > 1 ? parts[1].Trim() : null,
                Sku = parts.Length > 2 ? parts[2].Trim() : null,
                CostPrice = cost,
                SalePrice = sale,
                ItemType = itemType,
                IsActive = true
            });
        }
        await _itemRepo.InsertManyAsync(items, autoSave: true);
    }

    public async Task<List<ItemDto>> SearchAsync(string? filter, int maxCount = 20)
    {
        var query = await _itemRepo.GetQueryableAsync();
        query = query.Where(i => i.IsActive);
        if (!string.IsNullOrWhiteSpace(filter))
            query = query.Where(i => i.Name.Contains(filter) || (i.Barcode != null && i.Barcode.Contains(filter)));
        var items = query.Take(maxCount).ToList();
        return ObjectMapper.Map<List<Item>, List<ItemDto>>(items);
    }
}
